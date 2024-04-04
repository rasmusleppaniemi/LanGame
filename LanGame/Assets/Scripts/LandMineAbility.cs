using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class LandMineAbility : MonoBehaviour
{
    public GameObject LandminePrefab;
    public Transform abilityCallerTransform;
    public float LandmineCooldown;
    private bool isCooldown = false;

    public PlayerController playerController;

    public void OnLandMine(InputAction.CallbackContext context)
    {
        if (!isCooldown && playerController.grounded)
        {
            Vector3 spawnPosition = abilityCallerTransform.position;
            GameObject landMine = Instantiate(LandminePrefab, spawnPosition, Quaternion.identity);

            StartCoroutine(StartCooldown());
        }
    }

    IEnumerator StartCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(LandmineCooldown);
        isCooldown = false;
    }

    public void InterruptCooldown()
    {
        if (isCooldown)
        {
            StopCoroutine("StartCooldown");
            isCooldown = false;
        }
    }
}
