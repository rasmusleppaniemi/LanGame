using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameReadyManager : MonoBehaviour
{
    public PointSpawner pointSpawner;
    public List<PlayerPoints> playerPoints;
    public int minPlayers;
    public bool ready;
    public float countdownDuration = 5f;
    private float currentCountdown;

    public float minX = -20f;
    public float maxX = 20f;
    public float minZ = -20f;
    public float maxZ = 20f;

    public TextMeshProUGUI countdownText;
    public GameObject endScreen;

    private void Start()
    {
        StartCoroutine(CheckPlayers());
        UpdateCountdownText();
        endScreen.SetActive(false);
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
                PlayerPoints playerPointsScript = playerObject.GetComponent<PlayerPoints>();

                if (playerIndexScript != null && playerPointsScript != null && !playerPoints.Contains(playerPointsScript))
                {
                    // Add the PlayerPoints script to the playerPoints list
                    playerPoints.Add(playerPointsScript);
                }

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
            if(!ready)
            {
                countdownText.text = "Not Enough Players";
            }
            else
            {
                countdownText.text = currentCountdown.ToString();
            }
        }
    }

    private void StartGame()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        TeleportPlayers(players);
        pointSpawner.ActivateCoroutine();
    }
    public void EndGame()
    {
        DisplayEndScreen();
    }

    private void DisplayEndScreen()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerObject in players)
        {
            PlayerPoints playerPoints = playerObject.GetComponent<PlayerPoints>();
            PlayerIndexScript playerIndexScript = playerObject.GetComponent<PlayerIndexScript>();
            endScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    void TeleportPlayers(GameObject[] players)
    {
        countdownText.gameObject.SetActive(false);
        foreach (GameObject player in players)
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            Vector3 newPosition = new Vector3(randomX, 0f, randomZ);

            player.transform.position = newPosition;
        }
    }
}
