using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // drag 3 ghost prefabs here
    public float spawnRadius = 20f;
    public float initialDelay = 2f;
    public float spawnInterval = 5f;
    public int enemiesPerWave = 3;
    public float difficultyRamp = 0.95f; // decrease interval over time

    private float currentInterval;

    void Start()
    {
        currentInterval = spawnInterval;
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            SpawnWave();
            yield return new WaitForSeconds(currentInterval);

            // increase difficulty
            enemiesPerWave += 1;
            currentInterval *= difficultyRamp; // faster spawns
            currentInterval = Mathf.Clamp(currentInterval, 1f, 999f);
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector3 spawnPos = transform.position + Random.onUnitSphere * spawnRadius;
            spawnPos.y = 0f; // make sure it's on ground

            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomIndex], spawnPos, Quaternion.identity);
        }
    }
}
