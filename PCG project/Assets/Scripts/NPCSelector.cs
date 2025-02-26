using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WeightedNPC
{
    public GameObject prefab;
    public float weight = 1f;
}

public class NPCSelector : MonoBehaviour
{
    public WeightedNPC[] weightedNPCs;
    public Transform player;

    private float minSpawn = 5f;
    private float maxSpawn = 15f; 
    private float nextSpawnDistance;
    private float lastSpawnX;
    private float despawnDistance = 25f;
    
    private List<GameObject> activeNPCs = new List<GameObject>();

    void Start()
    {
        lastSpawnX = player.position.x;
        nextSpawnDistance = Random.Range(minSpawn, maxSpawn);
        SpawnRandomNPC();
    }

    void FixedUpdate()
    {
        float distance = Mathf.Abs(player.position.x - lastSpawnX);
        if (distance >= nextSpawnDistance)
        {
            lastSpawnX = player.position.x;
            nextSpawnDistance = Random.Range(minSpawn, maxSpawn);
            SpawnRandomNPC();
        }

        // Check all active NPCs for despawning
        CheckForDespawns();
    }

    void CheckForDespawns()
    {
        // Create a list to store NPCs that need to be removed
        List<GameObject> npcsToRemove = new List<GameObject>();

        foreach (GameObject npc in activeNPCs)
        {
            if (npc != null)
            {
                float distanceToNPC = Mathf.Abs(player.position.x - npc.transform.position.x);
                float direction = Mathf.Sign(player.position.x - npc.transform.position.x);
                float playerDirection = Mathf.Sign(Input.GetAxisRaw("Horizontal"));

                // Only despawn if the player has passed the NPC and is moving away from it
                if (distanceToNPC > despawnDistance && (playerDirection == 0 || playerDirection == direction))
                {
                    npcsToRemove.Add(npc);
                }
            }
        }

        // Remove marked NPCs
        foreach (GameObject npc in npcsToRemove)
        {
            DespawnNPC(npc);
        }
    }

    void SpawnRandomNPC()
    {
        if (weightedNPCs.Length == 0)
        {
            Debug.LogError("No NPC prefabs assigned!");
            return;
        }
        
        // Calculate total weight
        float totalWeight = 20f;
        foreach (WeightedNPC npc in weightedNPCs)
        {
            totalWeight += npc.weight;
        }

        // Pick a random value between 0 and total weight
        float randomValue = Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        // Select prefab based on weights
        GameObject selectedPrefab = weightedNPCs[0].prefab;
        foreach (WeightedNPC npc in weightedNPCs)
        {
            currentWeight += npc.weight;
            if (randomValue <= currentWeight)
            {
                selectedPrefab = npc.prefab;
                break;
            }
        }

        float direction = Mathf.Sign(Input.GetAxisRaw("Horizontal"));
        if (direction == 0)
        {
            direction = 1;
        }
        
        Vector3 spawnPosition = new Vector3(
            player.position.x + (nextSpawnDistance * direction), 
            selectedPrefab.transform.position.y, 
            0
        );

        GameObject newNPC = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
        activeNPCs.Add(newNPC);
    }

    void DespawnNPC(GameObject npc)
    {
        activeNPCs.Remove(npc);
        Destroy(npc);
    }
}
