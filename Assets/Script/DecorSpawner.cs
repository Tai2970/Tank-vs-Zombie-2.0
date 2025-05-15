using UnityEngine;
using System.Collections.Generic;

public class DecorSpawner : MonoBehaviour
{
    [Header("Sky Decorations (Air)")]
    public GameObject[] airDecorPrefabs;
    public float cloudFloatSpeed = 0.5f;
    public float cloudFloatAmplitude = 0.5f;
    public float cloudFloatFrequency = 1f;
    public float cloudMaxDistanceFromPlayer = 40f;
    public float cloudFadeSpeed = 0.5f;
    public float cloudSpawnInnerRadius = 5f;
    public float cloudSpawnOuterRadius = 20f;
    public int cloudsPerInterval = 3;

    [Header("Ground Decorations (Dynamic)")]
    public GameObject[] groundDecorPrefabs;
    public float groundSpawnRadius = 25f;
    public float groundSpawnInnerRadius = 4f;
    public float groundLifetime = 6f;
    public float groundCheckRadius = 3f;
    public int groundSpawnCount = 12;

    [Header("Timing")]
    public float checkInterval = 0.3f;

    private float timer;
    private List<CloudFloat> movingClouds = new List<CloudFloat>();

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= checkInterval)
        {
            timer = 0f;
            TrySpawnClouds();
            TrySpawnGroundDecor();
        }

        foreach (var cloud in movingClouds)
        {
            if (cloud == null || cloud.cloudObject == null) continue;

            float floatOffset = Mathf.Sin(Time.time * cloudFloatFrequency + cloud.randomOffset) * cloudFloatAmplitude;
            Vector3 drift = new Vector3(cloudFloatSpeed * Time.deltaTime, floatOffset * Time.deltaTime, 0f);
            cloud.cloudObject.transform.Translate(drift, Space.World);

            if (cloud.player != null)
            {
                float distance = Vector3.Distance(cloud.cloudObject.transform.position, cloud.player.position);
                if (distance > cloudMaxDistanceFromPlayer)
                    cloud.isFading = true;
            }

            if (cloud.isFading)
                FadeAndDestroyCloud(cloud);
        }
    }

    void TrySpawnClouds()
    {
        Transform targetPlayer = GetRandomActivePlayer();
        if (targetPlayer == null || airDecorPrefabs.Length == 0) return;

        for (int i = 0; i < cloudsPerInterval; i++)
        {
            // Spawn clouds in donut range near player
            Vector2 circle = Random.insideUnitCircle.normalized * Random.Range(cloudSpawnInnerRadius, cloudSpawnOuterRadius);
            Vector3 spawnPos = targetPlayer.position + new Vector3(circle.x, 10f, circle.y); // 10f height for clouds

            GameObject prefab = airDecorPrefabs[Random.Range(0, airDecorPrefabs.Length)];
            GameObject cloud = Instantiate(prefab, spawnPos, Quaternion.identity);

            movingClouds.Add(new CloudFloat(cloud, targetPlayer));
        }
    }

    void TrySpawnGroundDecor()
    {
        Transform targetPlayer = GetRandomActivePlayer();
        if (targetPlayer == null || groundDecorPrefabs.Length == 0) return;

        for (int i = 0; i < groundSpawnCount; i++)
        {
            Vector2 circle = Random.insideUnitCircle.normalized * Random.Range(groundSpawnInnerRadius, groundSpawnRadius);
            Vector3 spawnPos = targetPlayer.position + new Vector3(circle.x, 0f, circle.y);

            spawnPos.y = Terrain.activeTerrain != null
                ? Terrain.activeTerrain.SampleHeight(spawnPos)
                : targetPlayer.position.y;

            if (!Physics.CheckSphere(spawnPos, groundCheckRadius))
            {
                GameObject prefab = groundDecorPrefabs[Random.Range(0, groundDecorPrefabs.Length)];
                GameObject decor = Instantiate(prefab, spawnPos, Quaternion.Euler(0, Random.Range(0, 360f), 0));
                Destroy(decor, groundLifetime);
            }
        }
    }

    void FadeAndDestroyCloud(CloudFloat cloud)
    {
        Renderer renderer = cloud.cloudObject.GetComponent<Renderer>();
        if (renderer != null && renderer.material.HasProperty("_Color"))
        {
            Color color = renderer.material.color;
            color.a -= Time.deltaTime * cloudFadeSpeed;
            renderer.material.color = color;

            if (color.a <= 0f)
            {
                Destroy(cloud.cloudObject);
                cloud.cloudObject = null;
            }
        }
        else
        {
            Destroy(cloud.cloudObject);
            cloud.cloudObject = null;
        }
    }

    Transform GetRandomActivePlayer()
    {
        List<GameObject> candidates = new List<GameObject>();

        GameObject p1 = GameObject.FindGameObjectWithTag("Player");
        GameObject p2 = GameObject.FindGameObjectWithTag("Player2");
        GameObject ai = GameObject.FindGameObjectWithTag("AI");

        if (p1 && p1.activeInHierarchy) candidates.Add(p1);
        if (p2 && p2.activeInHierarchy) candidates.Add(p2);
        if (ai && ai.activeInHierarchy) candidates.Add(ai);

        if (candidates.Count == 0) return null;
        return candidates[Random.Range(0, candidates.Count)].transform;
    }

    private class CloudFloat
    {
        public GameObject cloudObject;
        public Transform player;
        public float randomOffset;
        public bool isFading = false;

        public CloudFloat(GameObject obj, Transform target)
        {
            cloudObject = obj;
            player = target;
            randomOffset = Random.Range(0f, Mathf.PI * 2f);
        }
    }
}


