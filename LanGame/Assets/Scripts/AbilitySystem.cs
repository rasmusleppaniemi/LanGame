using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilitySystem : MonoBehaviour
{
    public int SelectedAbility;
    private bool isSwitchingAbility;
    private float debounceTime = 0.2f; // Adjust this value as needed

    public AbilitySystem abilitySystem;
    public LandMineAbility landMineAbility;
    public PillarAbility pillarAbility;

    public bool CastingAbility;
    public void OnFire(InputAction.CallbackContext context)
    {
        switch (abilitySystem.SelectedAbility)
        {
            case 0:
                pillarAbility.OnPillar(context);
                break;
            case 1:
                landMineAbility.OnLandMine(context);
                break;
        }
    }

    private void Start()
    {
        SelectAbility();
    }

    public void OnAbilitySwitch(InputAction.CallbackContext context)
    {
        if (!isSwitchingAbility && !CastingAbility) // Check if the Pillar ability is casting
        {
            StartCoroutine(DebounceInput());

            // Interrupt the cooldown of the landmine ability
            if (SelectedAbility != 1) // Assuming 1 is the index for the landmine ability
            {
                landMineAbility.InterruptCooldown();
            }
        }
    }


    IEnumerator DebounceInput()
    {
        isSwitchingAbility = true;
        SelectedAbility = (SelectedAbility + 1) % transform.childCount;
        SelectAbility();

        yield return new WaitForSeconds(debounceTime);

        isSwitchingAbility = false;
    }

    void SelectAbility()
    {
        if (transform.childCount == 0)
        {
            Debug.LogWarning("No abilities found.");
            return;
        }

        int i = 0;
        foreach (Transform ability in transform)
        {
            ability.gameObject.SetActive(i == SelectedAbility);
            i++;
        }
    }
}
