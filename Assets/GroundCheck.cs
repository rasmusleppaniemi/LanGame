using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;
    public LayerMask groundLayer; // Define which layers are considered as ground'
    public float Range;

    private void FixedUpdate()
    {
        RaycastHit hit;
        // Cast a ray downwards from the player's position
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Range, groundLayer))
        {
            Debug.DrawRay(transform.position, Vector3.down * Range, Color.green);
            playerController.SetGrounded(true);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * Range, Color.red);
            playerController.SetGrounded(false);
        }
    }
}
