using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject interactionRing; // The gray ring around the object
    private bool isPlayerInRange = false; // To check if the player is in range of the object
    private IInteractable interactable; // Reference to the interactable interface

    void Start()
    {
        // Ensure the interaction ring is initially hidden
        interactionRing.SetActive(false);
        interactable = GetComponent<IInteractable>(); // Get the IInteractable component
    }

    void Update()
    {
        // Check if the player is in range and if the "E" key is pressed
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Call the Interact method if the interactable is set
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the interaction range
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionRing.SetActive(true);  // Show the gray ring when the player is close
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player has left the interaction range
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionRing.SetActive(false); // Hide the gray ring when the player is far away
        }
    }
}
