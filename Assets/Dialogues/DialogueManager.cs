using UnityEngine;
using UnityEngine.UI; // Include the UI namespace
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox; // Reference to the dialogue panel
    public TMP_Text dialogueText; // Reference to the text component
    private Queue<string> sentences; // Queue to hold sentences

    void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false); // Hide dialogue box initially
    }

    void Update()
    {
        // Check for input to display next sentence
        if (dialogueBox.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            DisplayNextSentence(); // Display the next sentence
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null)
        {
            Debug.LogError("Dialogue is null!");
            return;
        }

        Debug.Log("Starting dialogue: ");
        dialogueBox.SetActive(true); // Show dialogue box
        sentences.Clear(); // Clear previous sentences

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); // Enqueue each sentence
        }

        // Display the first sentence immediately
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue(); // End dialogue if no sentences left
            return;
        }

        string sentence = sentences.Dequeue(); // Get the next sentence
        StartCoroutine(TypeSentence(sentence)); // Start the typing coroutine
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = ""; // Clear current text

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(0.05f); // Wait for a short duration between letters (adjust as needed)
        }
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false); // Hide the dialogue box
        Debug.Log("Dialogue ended.");
    }
}