using UnityEngine;
using TMPro;

public class PlayerIndexScript : MonoBehaviour
{
    [SerializeField] private static int playerIndex = 0; // Default player index
    private TextMeshPro playerNameText;

    private void Awake()
    {
        // Get the TextMeshPro component attached to this GameObject
        playerNameText = GetComponentInChildren<TextMeshPro>();
        if (playerNameText == null)
        {
            Debug.LogError("PlayerIndexScript: TextMeshPro component not found in children!");
        }
        else
        {
            SetPlayerIndex(playerIndex);
            UpdatePlayerName();
        }
    }

    // Method to set the player index
    public void SetPlayerIndex(int index)
    {
        playerIndex = index;
        UpdatePlayerName();
        playerIndex++;
    }

    // Method to get the player index
    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    // Method to update the displayed player name based on the player index
    private void UpdatePlayerName()
    {
        if (playerNameText != null)
        {
            playerNameText.text = "Player " + playerIndex;
        }
    }
}
