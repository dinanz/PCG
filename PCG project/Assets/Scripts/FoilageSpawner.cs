using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoilageSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject cam;

    int[] weights = { 50, 30, 20 };
    List<GameObject> spawnedObjects = new List<GameObject>();
    float minSpawnDistance = 10f;
    float maxSpawnDistance = 15f;
    float spawnThreshold = 12f;
    float lastSpawnX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastSpawnX = cam.transform.position.x;

        // Spawn one so its not too empty
        GameObject selectedPrefab = prefabs[Random.Range(0, prefabs.Length)];
        Vector3 spawnPosition = new Vector3(cam.transform.position.x, 0, 0);
        GameObject spawned = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
        spawnedObjects.Add(spawned);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(cam.transform.position.x - lastSpawnX) > spawnThreshold)
        {
            SpawnFoliage();
            lastSpawnX = cam.transform.position.x;
        }

        CleanupFoliage();
    }

    void SpawnFoliage()
    {
        float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPosition;

        // Right
        if (cam.transform.position.x > lastSpawnX)
        {
            spawnPosition = new Vector3(cam.transform.position.x + spawnDistance, 0, 0);
            GameObject selectedPrefabRight = GetWeightedRandomPrefab();

            if (selectedPrefabRight != null)
            {
                GameObject spawnedRight = Instantiate(selectedPrefabRight, spawnPosition, Quaternion.identity);
                spawnedObjects.Add(spawnedRight);
            }
        }
        // Left
        else if (cam.transform.position.x < lastSpawnX)
        {
            spawnPosition = new Vector3(cam.transform.position.x - spawnDistance, 0, 0);
            GameObject selectedPrefabLeft = GetWeightedRandomPrefab();

            if (selectedPrefabLeft != null)
            {
                GameObject spawnedLeft = Instantiate(selectedPrefabLeft, spawnPosition, Quaternion.identity);
                spawnedObjects.Add(spawnedLeft);
            }
        }
    }

    void CleanupFoliage()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i].transform.position.x < cam.transform.position.x - 20f ||
                spawnedObjects[i].transform.position.x > cam.transform.position.x + 20f)
            {
                Destroy(spawnedObjects[i]);
                spawnedObjects.RemoveAt(i);
            }
        }
    }

    GameObject GetWeightedRandomPrefab()
    {
        int totalWeight = 0;
        foreach (int weight in weights)
            totalWeight += weight;

        int randomValue = Random.Range(0, totalWeight);

        int cumulativeWeight = 0;

        for (int i = 0; i < prefabs.Length; i++)
        {
            cumulativeWeight += weights[i];

            if (randomValue < cumulativeWeight)
            {
                return prefabs[i];
            }
        }
        return null;
    }
}