using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroy : MonoBehaviour
{
    public float DestroyAfterTime;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(DestroyAfterTime);
        Debug.Log("Destroyed");
        Destroy(gameObject);
    }
}
