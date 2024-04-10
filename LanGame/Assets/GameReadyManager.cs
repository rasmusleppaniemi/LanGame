using UnityEngine;
using TMPro;
using System.Collections;

public class GameReadyManager : MonoBehaviour
{
    public PointSpawner pointSpawner;
    public int minPlayers;
    public bool ready;
    public float countdownDuration = 5f;
    private float currentCountdown;

    public float minX = -20f;
    public float maxX = 20f;
    public float minZ = -20f;
    public float maxZ = 20f;

    public TextMeshProUGUI countdownText;

    private void Start()
    {
        StartCoroutine(CheckPlayers());
    }
    public void Update()
    {
        CheckPlayers();
    }
    private IEnumerator CheckPlayers()
    {
        while (!ready)
        {
            yield return new WaitForSeconds(0.2f);

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            int playersReady = 0;

            foreach (GameObject playerObject in players)
            {
                PlayerIndexScript playerIndexScript = playerObject.GetComponent<PlayerIndexScript>();
                if (playerIndexScript != null)
                {
                    playersReady++;
                }
            }

            if (playersReady >= minPlayers)
            {
                ready = true;
            }

            yield return null;
        }

        StartCountdown();
    }

    private void StartCountdown()
    {
        currentCountdown = countdownDuration;
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        while (currentCountdown > 0f)
        {
            yield return new WaitForSeconds(1f);
            currentCountdown -= 1f;
            UpdateCountdownText();
        }

        StartGame();
    }

    private void UpdateCountdownText()
    {
        if (countdownText != null)
        {
            countdownText.text = currentCountdown.ToString();
        }
    }

    private void StartGame()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        TeleportPlayers(players);
        pointSpawner.ActivateCoroutine();
    }

    void TeleportPlayers(GameObject[] players)
    {
        foreach (GameObject player in players)
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            Vector3 newPosition = new Vector3(randomX, 0f, randomZ);

            player.transform.position = newPosition;
        }
    }
}
