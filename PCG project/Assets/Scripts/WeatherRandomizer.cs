using UnityEngine;

public class WeatherSpawner : MonoBehaviour
{
    public GameObject snowPrefab;
    public GameObject rainPrefab;
    private GameObject player; // Player reference
    public Vector3 weatherScale = new Vector3(0.7f, 0.7f, 0.7f); // Adjust weather size

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find player via tag

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure your player has the correct tag.");
            return;
        }

        SpawnWeather();
    }

    void SpawnWeather()
    {
        int randomWeather = Random.Range(0, 3); // 0 = Clear, 1 = Rain, 2 = Snow
        GameObject spawnedWeather = null;

        if (randomWeather == 1 && rainPrefab != null)
        {
            Quaternion rainRotation = Quaternion.Euler(0, 0, -30f); // Tilt rain to 1 o’clock (30°)
            spawnedWeather = Instantiate(rainPrefab, player.transform.position, rainRotation);
        }
        else if (randomWeather == 2 && snowPrefab != null)
        {
            spawnedWeather = Instantiate(snowPrefab, player.transform.position, Quaternion.identity);
        }

        // Parent the weather to the player so it follows
        if (spawnedWeather != null)
        {
            spawnedWeather.transform.SetParent(player.transform);
            spawnedWeather.transform.localPosition = new Vector3(0, 2f, 0); // Keep weather above player
            spawnedWeather.transform.localScale = weatherScale; // Fix weather size
        }
    }
}
