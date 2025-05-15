using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject[] ghostPrefabs;  // Assign 3 ghost prefabs here
    public float spawnInterval = 2f;
    public int maxGhostsOnScreen = 6;
    public float minSpawnDistance = 3f;

    private List<GameObject> activeGhosts = new List<GameObject>();
    private Camera cam;
    private Transform player;

    void Start()
    {
        cam = Camera.main;
        StartCoroutine(WaitForPlayerThenSpawn());
    }

    IEnumerator WaitForPlayerThenSpawn()
    {
        // Wait until a player with the "Player" tag appears in the scene
        while (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
                StartCoroutine(SpawnLoop());
                yield break;
            }

            yield return null; // check again next frame
        }
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (activeGhosts.Count < maxGhostsOnScreen)
            {
                Vector3 spawnPos = GetRandomScreenPosition();

                // Avoid clumping
                bool tooClose = false;
                foreach (var ghost in activeGhosts)
                {
                    if (ghost == null) continue;
                    if (Vector3.Distance(ghost.transform.position, spawnPos) < minSpawnDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    GameObject ghostToSpawn = ghostPrefabs[Random.Range(0, ghostPrefabs.Length)];
                    GameObject ghost = Instantiate(ghostToSpawn, spawnPos, Quaternion.identity);
                    Debug.Log("Spawned ghost: " + ghost.name + " at " + spawnPos);

                    activeGhosts.Add(ghost);
                }
            }

            // Clean up removed/inactive ghosts
            activeGhosts.RemoveAll(g => g == null || !g.activeSelf);
        }
    }

    Vector3 GetRandomScreenPosition()
    {
        // Fallback in case player is still not set
        if (player == null) return transform.position;

        // Spawn at fixed Z distance between camera and player
        float zSpawn = player.position.z;

        Vector3 screenPos = new Vector3(
            Random.Range(0.1f, 0.9f),
            Random.Range(0.1f, 0.9f),
            cam.WorldToViewportPoint(new Vector3(0, 0, zSpawn)).z
        );

        Vector3 worldPos = cam.ViewportToWorldPoint(new Vector3(
            screenPos.x,
            screenPos.y,
            cam.nearClipPlane + 10f
        ));

        // Keep consistent height
        worldPos.y = player.position.y;

        return worldPos;
    }
}
