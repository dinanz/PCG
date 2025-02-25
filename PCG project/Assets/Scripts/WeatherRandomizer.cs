using UnityEngine;

public class WeatherSpawner : MonoBehaviour
{
    public GameObject snowPrefab;
    public GameObject rainPrefab;
    public GameObject player; // Assign player GameObject in Inspector

    void Start()
    {
        SpawnWeather();
    }

    void SpawnWeather()
    {
        int randomWeather = Random.Range(0, 3); // 0 = Clear, 1 = Rain, 2 = Snow
        GameObject spawnedWeather = null;

        if (randomWeather == 1 && rainPrefab != null)
        {
            Quaternion rainRotation = Quaternion.Euler(0, 0, -30f); // Tilt rain to 1 o’clock (30°)
            spawnedWeather = Instantiate(rainPrefab, transform.position, rainRotation);
        }
        else if (randomWeather == 2 && snowPrefab != null)
        {
            spawnedWeather = Instantiate(snowPrefab, transform.position, Quaternion.identity);
        }

        // Parent the weather to the player so it follows
        if (spawnedWeather != null && player != null)
        {
            spawnedWeather.transform.SetParent(player.transform);
        }
    }
}
