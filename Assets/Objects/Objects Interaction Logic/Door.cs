using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public GameObject doorPivot;

    public float openAngle = 90f; // The angle to which the door will open
    public float openSpeed = 80f; // Speed at which the door opens

    public bool isOpen = false; // Indicates if the door is currently open
    public bool opened = false;

    private Quaternion closedRotation; // Initial rotation of the door
    private Quaternion targetRotation; // Target rotation when opening


    public void Interact()
    {

        Debug.Log("Otwieranie drzwi");
        isOpen = true;

        // Store the initial rotation of the door
        closedRotation = transform.localRotation;

        // Set the target rotation based on the open angle
        targetRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);

    }

    void Update()
    {
        // If the door is open, rotate towards the target rotation
        if (isOpen && !opened)
        {
            // Rotate the door smoothly to the target rotation
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, openSpeed * Time.deltaTime);

            // If the door is close to the target rotation, stop opening
            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
            {
                transform.localRotation = targetRotation; // Snap to target to avoid jittering
                opened = true;
            }
        }
    }

}
