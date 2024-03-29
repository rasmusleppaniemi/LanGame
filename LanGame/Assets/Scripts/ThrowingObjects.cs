using UnityEngine;

public class ThrowingObjects : MonoBehaviour
{
    public float throwForce = 10f;

    public void ThrowPlayer(GameObject playerObject)
    {
        if (playerObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = playerObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // Calculate the direction away from this object
                Vector3 direction = playerObject.transform.position - transform.position;
                direction.Normalize();

                // Apply the force to the player's Rigidbody
                playerRigidbody.AddForce(direction * throwForce, ForceMode.Impulse);
            }
        }
    }
}