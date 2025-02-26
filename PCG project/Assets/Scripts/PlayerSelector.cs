using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public GameObject[] playerPrefabs; // Assign prefabs in the inspector
    private GameObject currentPlayer;

    void Start()
    {
        SpawnRandomPlayer();
    }

    void SpawnRandomPlayer()
    {
        if (playerPrefabs.Length == 0)
        {
            Debug.LogError("No player prefabs assigned!");
            return;
        }

        // Pick a random prefab
        GameObject selectedPrefab = playerPrefabs[Random.Range(0, playerPrefabs.Length)];

        // Instantiate at a chosen position (adjust as needed)
        currentPlayer = Instantiate(selectedPrefab, transform.position, Quaternion.identity);
    }
}
