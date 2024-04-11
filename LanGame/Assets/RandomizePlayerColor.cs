using UnityEngine;
using System.Collections.Generic;

public class RandomizePlayerColor : MonoBehaviour
{
    public Color[] availableColors; // List of available colors

    private Renderer playerRenderer;
    private static List<Color> selectedColors = new List<Color>(); // List to store selected colors

    void Start()
    {
        // Get the renderer component of the player object
        playerRenderer = GetComponent<Renderer>();

        // Call the function to randomize the color
        RandomizeColor();
    }

    void RandomizeColor()
    {
        // If all colors have been selected, reset the list
        if (selectedColors.Count >= availableColors.Length)
        {
            selectedColors.Clear();
        }

        // Pick a random color from availableColors that hasn't been selected
        Color randomColor = availableColors[Random.Range(0, availableColors.Length)];
        while (selectedColors.Contains(randomColor))
        {
            randomColor = availableColors[Random.Range(0, availableColors.Length)];
        }

        // Add the selected color to the list
        selectedColors.Add(randomColor);

        // Assign the new color to the player's renderer
        playerRenderer.material.color = randomColor;
    }
}
