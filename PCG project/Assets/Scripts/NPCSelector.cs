using UnityEngine;

[System.Serializable]
public class WeightedNPC
{
    public GameObject prefab;
    public float weight = 1f;
}

public class NPCSelector : MonoBehaviour
{
    public WeightedNPC[] weightedNPCs; // Assign prefabs and weights in the inspector
    private GameObject currentNPC;
    public Transform player; // so we can track where to spawn NPC

    private float minSpawn = 5f; // set npc spawn range
    private float maxSpawn = 15f; 

    private float nextSpawnDistance; // set point
    private float lastSpawnX; 
    private float despawnDistance = 25f;

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
        // removes old npc from scene
        if (currentNPC != null)
        {
            if (despawnDistance <= Mathf.Abs(player.position.x - currentNPC.transform.position.x))
            {
                DespawnNPC();
            }
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
        float totalWeight = 0f;
        foreach (WeightedNPC npc in weightedNPCs)
        {
            totalWeight += npc.weight;
        }

        // Pick a random value between 0 and total weight
        float randomValue = Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        // Select prefab based on weights
        GameObject selectedPrefab = weightedNPCs[0].prefab; // Default to first prefab
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
        Debug.Log(direction);
        if (direction == 0) // player isn't moving
        {
            direction = Mathf.Sign(direction);
        }
        Vector3 spawnPosition = new Vector3(player.position.x + (nextSpawnDistance*direction), selectedPrefab.transform.position.y, 0);
        // Instantiate in relation to the player
        if (currentNPC != null)
        {
            DespawnNPC();
        }
        currentNPC = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
        Debug.Log(currentNPC.name + " was instatiated at " + currentNPC.transform.position.x);

        Debug.Log("and player is at " + player.position.x + ", " + player.position.y);

    }

    void DespawnNPC()
    {
        Debug.Log("Destroying " + currentNPC.name + " at " + currentNPC.transform.position.x);
        Destroy(currentNPC);
    }
}
