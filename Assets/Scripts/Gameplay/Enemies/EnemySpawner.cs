using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Round> rounds;
    public float spawnInterval = 2.0f;
    private WaypointsManager waypointsManager;
    private int currentRoundIndex = 0;
    private int currentEnemyIndex = 0;
    private int enemiesRemainingToSpawn;
    private int enemiesRemainingAlive;

    public int CurrentRoundNumber
    {
        get { return currentRoundIndex + 1; } // +1 to make it 1-based instead of 0-based
    }

    void Start()
    {
        waypointsManager = FindObjectOfType<WaypointsManager>();
        if (waypointsManager != null && waypointsManager.waypoints.Length > 0)
        {
            StartRound();
        }
    }

    void StartRound()
    {
        if (currentRoundIndex < rounds.Count)
        {
            enemiesRemainingToSpawn = 0;
            foreach (var enemyTypeCount in rounds[currentRoundIndex].enemies)
            {
                enemiesRemainingToSpawn += enemyTypeCount.count;
            }
            enemiesRemainingAlive = enemiesRemainingToSpawn;
            InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
        }
        else
        {
            Debug.Log("All rounds completed!");
        }
    }

    void SpawnEnemy()
    {
        if (enemiesRemainingToSpawn > 0)
        {
            var round = rounds[currentRoundIndex];
            var enemyTypeCount = round.enemies[currentEnemyIndex];
            if (enemyTypeCount.count > 0)
            {
                Transform spawnPoint = waypointsManager.waypoints[0];
                GameObject enemy = Instantiate(enemyTypeCount.enemyPrefab, spawnPoint.position, Quaternion.Euler(0, 0, 0), transform);
                enemy.GetComponent<EnemyMovement>().SetInitialWaypoint();
                enemy.GetComponent<EnemyHealth>().OnDeath += OnEnemyDeath;
                enemyTypeCount.count--;
                enemiesRemainingToSpawn--;
                Debug.Log($"Spawned enemy.");
            }
            else
            {
                currentEnemyIndex++;
                if (currentEnemyIndex >= round.enemies.Count)
                {
                    currentEnemyIndex = 0;
                }
            }
        }
        else
        {
            CancelInvoke("SpawnEnemy");
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;
        if (enemiesRemainingAlive <= 0)
        {
            currentRoundIndex++;
            StartRound();
        }
    }
}