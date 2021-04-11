using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject[] StandingPlatformPrefabs;
    public GameObject[] MovingPlatformPrefabs;
    public GameObject[] RotatingPlatformPrefabs;

    public GameObject[] StandingTrapsPrefabs;
    public GameObject[] MovingTrapsPrefabs;
    public GameObject[] RotatingTrapsPrefabs;

    public int trapChance = 30;

    public float movingChance = 40f;
    public float rotatingChance = 70f;

    public float spawnRate = 1.5f; // spawn platforms in seconds

    [SerializeField]
    private float verticalOffset = -6f;

    [SerializeField]
    private float spawnHorizontalDistance = 4f; // -2 = left 2 = right 

    public bool spawning = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnRate, spawnRate);
    }

    void SpawnTrap()
    {
        GameObject trapPrefab;
        int typeChance = Random.Range(0, 101);

        float newMovingChance = Mathf.Clamp((movingChance + GameController.distanceTraveled / 100f), 10f, 50f);
        float newRotateChance = Mathf.Clamp((rotatingChance + GameController.distanceTraveled / 100f), 20f, 100f);

        if (typeChance < newMovingChance)
            trapPrefab = MovingTrapsPrefabs[Random.Range(0, MovingTrapsPrefabs.Length)];
        else if (typeChance < newRotateChance)
            trapPrefab = RotatingTrapsPrefabs[Random.Range(0, RotatingTrapsPrefabs.Length)];
        else trapPrefab = StandingTrapsPrefabs[Random.Range(0, StandingTrapsPrefabs.Length)];

        var spawnedPlatform = Instantiate(trapPrefab, new Vector3(Random.Range(-spawnHorizontalDistance, spawnHorizontalDistance), verticalOffset, 0), Quaternion.identity);
        spawnedPlatform.transform.parent = this.transform;
    }

    void SpawnPlatform()
    {
        GameObject platformPrefab;
        int typeChance = Random.Range(0, 101);

        float newMovingChance = Mathf.Clamp((movingChance + GameController.distanceTraveled / 100f), 10f, 50f);
        float newRotateChance = Mathf.Clamp((rotatingChance + GameController.distanceTraveled / 100f), 20f, 100f);

        if (typeChance < newMovingChance)
            platformPrefab = MovingPlatformPrefabs[Random.Range(0, MovingPlatformPrefabs.Length)];
        else if (typeChance < newRotateChance)
            platformPrefab = RotatingPlatformPrefabs[Random.Range(0, RotatingPlatformPrefabs.Length)];
        else platformPrefab = StandingPlatformPrefabs[Random.Range(0, StandingPlatformPrefabs.Length)];

        var spawnedPlatform = Instantiate(platformPrefab, new Vector3(Random.Range(-spawnHorizontalDistance, spawnHorizontalDistance), verticalOffset, 0), Quaternion.identity);
        spawnedPlatform.transform.parent = this.transform;
    }

    void Spawn()
    {
        if (!GameController.GameStarted || !spawning)
            return;

        int rndChance = Random.Range(0, 120);

        if (rndChance <= trapChance)// Trap
            SpawnTrap();
        else if (rndChance <= 100) // Platform
            SpawnPlatform();
        // else skip spawn
    }
}
