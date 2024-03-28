using System.Collections;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    public bool armed = false;
    [SerializeField] float ArmTime;
    ThrowingObjects ThrowingObjectsScript;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(ArmTime);
        armed = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (armed && other.CompareTag("Player"))
        {
            ThrowingObjectsScript = FindObjectOfType<ThrowingObjects>();

            if (ThrowingObjectsScript != null)
            {
                ThrowingObjectsScript.ThrowPlayer(other.gameObject);
            }

            Destroy(gameObject);
        }
    }
}