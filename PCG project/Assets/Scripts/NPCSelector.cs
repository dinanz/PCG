using UnityEngine;

public class NPCSelector : MonoBehaviour
{
    public GameObject[] NPCPrefabs; // Assign prefabs in the inspector
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
        if (NPCPrefabs.Length == 0)
        {
            Debug.LogError("No NPC prefabs assigned!");
            return;
        }
        
        // Pick a random prefab
        GameObject selectedPrefab = NPCPrefabs[Random.Range(0, NPCPrefabs.Length)];
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
