using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PillarAbility : MonoBehaviour
{
    public GameObject pillarPrefab;
    public Transform abilityCallerTransform;
    public float spawnDistance = 3f;
    public int numPillars = 3;
    public float pillarInterval = 2f;
    public float cooldown = 5f;
    private bool isCooldown = false;
    public ParticleSystem groundBurstEffect;
    public float riseSpeed = 1f;
    public PlayerController playerController;
    public bool SelectedWeapon;

    public void OnPillars(InputAction.CallbackContext context)
    {
        if (!isCooldown && playerController.grounded && SelectedWeapon)
        {
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
        isCooldown = true;

        yield return new WaitForSeconds(cooldown);

        isCooldown = false;
    }

    IEnumerator ActivatePillarsWithDelay()
    {
        if (abilityCallerTransform == null)
        {
            Debug.LogError("Player transform not assigned to PillarAbility script.");
            yield break;
        }

        Vector3 spawnDirection = abilityCallerTransform.forward;

        Vector3 spawnPosition = abilityCallerTransform.position + spawnDirection * spawnDistance;
        spawnPosition -= Vector3.up * 10f;

        for (int i = 0; i < numPillars; i++)
        {
            GameObject pillar = Instantiate(pillarPrefab, spawnPosition, Quaternion.identity);

            spawnPosition += spawnDirection * (spawnDistance + pillarInterval);

            StartCoroutine(RisePillar(pillar.transform));

            yield return new WaitForSeconds(0.2f);
        }
        if (groundBurstEffect != null)
        {
            groundBurstEffect.Play();
        }
    }

    IEnumerator RisePillar(Transform pillarTransform)
    {
        float initialY = pillarTransform.position.y;

        float targetY = pillarTransform.position.y + 10f;

        Rigidbody rb = pillarTransform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        while (pillarTransform.position.y < targetY)
        {
            float newY = pillarTransform.position.y + riseSpeed * Time.deltaTime;
            pillarTransform.position = new Vector3(pillarTransform.position.x, newY, pillarTransform.position.z);
            yield return null;
        }
        rb.isKinematic = false;
    }
}
