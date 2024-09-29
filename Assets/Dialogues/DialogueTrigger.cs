using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue; // Reference to your Dialogue scriptable object
    private bool playerInRange;
    private bool dialogueStarted = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered dialogue range.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited dialogue range.");
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F) && !dialogueStarted)
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        Debug.Log("Dialogue Triggered!");
        dialogueStarted = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}