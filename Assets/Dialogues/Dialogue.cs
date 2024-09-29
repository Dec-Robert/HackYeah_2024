using System.Collections;
using UnityEngine;
using TMPro;
[System.Serializable]
public class Dialogue
{
    public string characterName;   // Name of the character speaking
    [TextArea(3, 10)]
    public string[] sentences;     // Array of sentences for the conversation
}