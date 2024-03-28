using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PillarAbility : MonoBehaviour
{
    public GameObject pillarPrefab;
    public Transform playerTransform; // Reference to the player's transform
    public float spawnDistance = 3f; // Distance from the player to spawn the pillars
    public int numPillars = 3; // Number of pillars to spawn
    public float pillarInterval = 2f; // Interval between each spawned pillar
    public float cooldown = 5f; // Cooldown duration
    private bool isCooldown = false; // Flag to track cooldown state
    public ParticleSystem groundBurstEffect;
    public float riseSpeed = 1f; // Speed at which the pillars rise
    public PlayerController playerController;

    public void OnPillars(InputAction.CallbackContext context)
    {
        // Check if the ability is on cooldown
        if (!isCooldown && playerController.grounded)
        {
            // Start the cooldown coroutine and activate the ability
            StartCoroutine(StartCooldown());
            StartCoroutine(ActivatePillarsWithDelay());
        }
        else
        {
            
            Debug.Log("Ability on cooldown!");
        }
    }

    IEnumerator StartCooldown()
    {
        // Set the cooldown flag to true
        isCooldown = true;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(cooldown);

        // Reset the cooldown flag
        isCooldown = false;
    }

    IEnumerator ActivatePillarsWithDelay()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player transform not assigned to PillarAbility script.");
            yield break;
        }

        // Get the forward direction of the player
        Vector3 spawnDirection = playerTransform.forward;

        // Calculate the initial spawn position (below the ground)
        Vector3 spawnPosition = playerTransform.position + spawnDirection * spawnDistance;
        spawnPosition -= Vector3.up * 10f; // Adjust this value to suit your scene's ground level

        // Spawn pillars with intervals
        for (int i = 0; i < numPillars; i++)
        {
            // Instantiate pillar at the current spawn position
            GameObject pillar = Instantiate(pillarPrefab, spawnPosition, Quaternion.identity);

            // Move the spawn position forward by the spawn distance plus the interval
            spawnPosition += spawnDirection * (spawnDistance + pillarInterval);

            // Move the pillar upwards gradually
            StartCoroutine(RisePillar(pillar.transform));

            // Wait for the specified interval before spawning the next pillar
            yield return new WaitForSeconds(0.2f);
        }

        // Trigger ground burst effect
        if (groundBurstEffect != null)
        {
            groundBurstEffect.Play();
        }
    }

    IEnumerator RisePillar(Transform pillarTransform)
    {
        // Get the initial Y position of the pillar
        float initialY = pillarTransform.position.y;

        // Calculate the target Y position (ground level)
        float targetY = pillarTransform.position.y + 10f; // Adjust this value to reach the desired height

        Rigidbody rb = pillarTransform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Gradually move the pillar upwards
        while (pillarTransform.position.y < targetY)
        {
            float newY = pillarTransform.position.y + riseSpeed * Time.deltaTime;
            pillarTransform.position = new Vector3(pillarTransform.position.x, newY, pillarTransform.position.z);
            yield return null;
        }
        rb.isKinematic = false;
    }
}
