using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    public int Points;
    public int MaxPoints;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            Points++;

            // Destroy the object with the "Point" tag
            Destroy(other.gameObject);
        }
    }
}
