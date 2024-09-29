using UnityEngine;

public class MirrorRotation : MonoBehaviour
{
    public float rotationAngle = 45f; // Amount of rotation per key press
    private bool playerInRange = false; // Detect if player is near
    private float currentRotationY = 45f; // Track the current Y-axis rotation

    void Update()
    {
        // If player is near and 'F' key is pressed
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            RotateMirror();
        }
    }

    void RotateMirror()
    {
        // Apply the rotation to the Y-axis explicitly
        transform.localRotation = Quaternion.Euler(0f, currentRotationY + rotationAngle, 0f);
        currentRotationY += rotationAngle;
        Debug.Log("Mirror rotated to " + currentRotationY + " degrees.");
    }

    // Detect when player enters the trigger collider attached to the mirror
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has the tag "Player"
        {
            playerInRange = true;
            Debug.Log("Player is near the mirror.");
        }
    }

    // Detect when player exits the trigger collider attached to the mirror
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left the mirror's range.");
        }
    }
}