using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    public int points = 0;
    public int maxPoints = 1;
    public string playerName;
    public GameReadyManager gameReadyManager;
    public bool HasCollected = false;

    private void Start()
    {
        gameReadyManager = FindObjectOfType<GameReadyManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point") && !HasCollected)
        {
            HasCollected = true;
            Debug.Log("Point Collected");
            points++;
            Destroy(other.gameObject);
        }

        if (points >= maxPoints)
        {
            if (gameReadyManager != null)
            {
                gameReadyManager.EndGame();
                Debug.Log(playerName + " reached maximum points.");
            }
            else
            {
                Debug.LogError("GameReadyManager reference not set in PlayerPoints script.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HasCollected = false;
    }
}
