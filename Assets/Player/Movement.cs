using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of movement
    public float rotationSpeed = 10f;  // Speed of rotation

    public GameObject playerModel;  // The 3D model of the player
    public Camera mainCamera;  // The camera, which is part of the parent
    public Rigidbody rb;

    // The amount of rotation needed to align the model (e.g., fix model lying down)
    private Quaternion modelRotationFix = Quaternion.Euler(90f, 0f, 0f); // Adjust if needed

    private Vector3 izoViewPosition = new Vector3(-6, 12, -6); // Adjust camera position

    void Update()
    {
        MovePlayer();
    }

    private void LateUpdate()
    {
        mainCamera.transform.position = playerModel.transform.position + izoViewPosition;
    }

    void MovePlayer()
    {
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionZ = Input.GetAxis("Vertical");

        Vector3 forward = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z).normalized;
        Vector3 right = new Vector3(mainCamera.transform.right.x, 0, mainCamera.transform.right.z).normalized;

        Vector3 move = (forward * moveDirectionZ + right * moveDirectionX).normalized;
        Vector3 movementVelocity = move * moveSpeed;

        rb.velocity = new Vector3(movementVelocity.x, rb.velocity.y, movementVelocity.z);

        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime) * modelRotationFix;
        }
    }
}
