using UnityEngine;
using TMPro;

public class PlayerIndexScript : MonoBehaviour
{
    private static int playerCount = 0;
    private TextMeshPro playerNameText;
    private int playerIndex;
    private PlayerPoints playerPoints; // Reference to PlayerPoints script

    private void Awake()
    {
        playerNameText = GetComponentInChildren<TextMeshPro>();
        if (playerNameText == null)
        {
            Debug.LogError("PlayerIndexScript: TextMeshPro component not found in children!");
        }
        else
        {
            playerIndex = ++playerCount;
            UpdatePlayerName();
        }

        playerPoints = GetComponent<PlayerPoints>(); // Get reference to PlayerPoints script
    }

    private void UpdatePlayerName()
    {
        if (playerNameText != null)
        {
            playerNameText.text = "Player " + playerIndex;
        }

        // Set player name in PlayerPoints script
        if (playerPoints != null)
        {
            playerPoints.playerName = playerNameText.text;
        }
    }
}
