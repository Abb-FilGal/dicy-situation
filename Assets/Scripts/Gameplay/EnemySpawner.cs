using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2.0f;
    private WaypointsManager waypointsManager;

    void Start()
    {
        waypointsManager = FindObjectOfType<WaypointsManager>();
        if (waypointsManager != null && waypointsManager.waypoints.Length > 0)
        {
            InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = waypointsManager.waypoints[0];
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.Euler(-90, 0, 0));
        enemy.GetComponent<EnemyMovement>().SetInitialWaypoint();
        Debug.Log($"Spawned enemy at {spawnPoint.position} with rotation {enemy.transform.rotation.eulerAngles}");
    }
}