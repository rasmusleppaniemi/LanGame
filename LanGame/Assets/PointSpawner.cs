using System.Collections;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public float spawnIntervals;
    public GameObject PointPrefab;
    public float minX = -20f;
    public float maxX = 20f;
    public float minZ = -20f;
    public float maxZ = 20f;
    public bool Activated;

    // Start is called before the first frame update
    void Start()
    {
        // Deactivate coroutine initially
        DeactivateCoroutine();
    }

    // Coroutine for spawning points
    private IEnumerator SpawnPointsCoroutine()
    {
        while (Activated)
        {
            yield return new WaitForSeconds(spawnIntervals);
            SpawnPoint();
        }
    }

    // Method to activate coroutine
    public void ActivateCoroutine()
    {
        Activated = true;
        StartCoroutine(SpawnPointsCoroutine());
    }

    // Method to deactivate coroutine
    public void DeactivateCoroutine()
    {
        Activated = false;
        StopAllCoroutines();
    }

    private void SpawnPoint()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 newPosition = new Vector3(randomX, 1f, randomZ);

        Instantiate(PointPrefab, newPosition, Quaternion.identity);
    }
}
