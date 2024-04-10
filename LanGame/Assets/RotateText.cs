using UnityEngine;
using TMPro;

public class RotateText : MonoBehaviour
{
    private void Update()
    {
        // Find all cameras in the scene
        Camera[] cameras = FindObjectsOfType<Camera>();

        // Ensure there are at least two cameras in the scene
        if (cameras.Length >= 2)
        {
            // Sort the cameras based on their distance to the text
            System.Array.Sort(cameras, (cam1, cam2) =>
                Vector3.Distance(transform.position, cam1.transform.position)
                .CompareTo(Vector3.Distance(transform.position, cam2.transform.position)));

            // Get the direction towards the second nearest camera
            Vector3 directionToSecondNearestCamera = (transform.position - cameras[1].transform.position).normalized;

            // Calculate the rotation to face away from the second nearest camera
            Quaternion targetRotation = Quaternion.LookRotation(directionToSecondNearestCamera, Vector3.up);

            // Apply the rotation
            transform.rotation = targetRotation;
        }
    }
}
