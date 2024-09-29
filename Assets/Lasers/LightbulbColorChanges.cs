using UnityEngine;

public class LightbulbColorChange : MonoBehaviour
{
    private Material bulbMaterial; // Material of the lightbulb
    private Color originalColor; // Store the original color

    void Start()
    {
        // Get the material of the lightbulb
        bulbMaterial = GetComponent<Renderer>().material;
        // Store the original color
        originalColor = bulbMaterial.color;
    }

    public void ChangeColor(Color newColor)
    {
        // Change the color of the lightbulb
        bulbMaterial.color = newColor;
    }

    public void ResetColor()
    {
        // Reset the color to original
        bulbMaterial.color = originalColor;
    }
}