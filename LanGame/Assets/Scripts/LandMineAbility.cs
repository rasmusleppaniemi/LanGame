using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class LandMineAbility : MonoBehaviour
{
    public GameObject LandMinePrefab;
    public Transform abilityCallerTransform;

    public void OnLandMine(InputAction.CallbackContext context)
    {
        Vector3 spawnPosition = abilityCallerTransform.position;
        GameObject landMine = Instantiate(LandMinePrefab, spawnPosition, Quaternion.identity);
    }
}
