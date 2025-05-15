using UnityEngine;
using System.Collections.Generic;

public class TankEnemySpawner : MonoBehaviour
{
    public GameObject enemyTankPrefab;
    public float spawnInterval = 3f;
    public float spawnRange = 25f;
    public int maxTanks = 6;

    private float spawnTimer;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            TrySpawnEnemy();
        }

        // Clean up destroyed enemies
        activeEnemies.RemoveAll(item => item == null);
    }

    void TrySpawnEnemy()
    {
        Transform player = FindClosestPlayer();
        if (player == null || activeEnemies.Count >= maxTanks)
            return;

        Vector3 randomOffset = new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
        Vector3 spawnPosition = player.position + randomOffset;

        GameObject newEnemy = Instantiate(enemyTankPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(newEnemy);
    }

    Transform FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] aiPlayers = GameObject.FindGameObjectsWithTag("AI");
        GameObject[] all = new GameObject[players.Length + aiPlayers.Length];
        players.CopyTo(all, 0);
        aiPlayers.CopyTo(all, players.Length);

        GameObject closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject p in all)
        {
            float dist = Vector3.Distance(transform.position, p.transform.position);
            if (dist < minDist)
            {
                closest = p;
                minDist = dist;
            }
        }

        return closest?.transform;
    }
}
