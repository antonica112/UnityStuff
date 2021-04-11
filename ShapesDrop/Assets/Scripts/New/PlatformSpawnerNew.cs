using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnerNew : MonoBehaviour
{
    public GameObject[] StandingPlatformPrefabs;
    public GameObject[] MovingPlatformPrefabs;
    public GameObject[] RotatingPlatformPrefabs;

    public GameObject[] StandingTrapsPrefabs;
    public GameObject[] MovingTrapsPrefabs;
    public GameObject[] RotatingTrapsPrefabs;

    public float horizontalLimit = 8f;

    public int trapChance = 30;

    public float movingChance = 40f;
    public float rotatingChance = 70f;

    public float distanceBetweenSpawnsChecks = 3f;

    public Transform player;

    public bool spawning = false;

    private float currentDistanceCheck = 0f;

    private float verticalDistanceToSpawnTo = 12f;

    void SpawnTrap(float yDistance)
    {
        GameObject trapPrefab;
        int typeChance = Random.Range(0, 101);

        //float newMovingChance = Mathf.Clamp((movingChance + GameController.distanceTraveled / 100f), 10f, 50f);
        //float newRotateChance = Mathf.Clamp((rotatingChance + GameController.distanceTraveled / 100f), 20f, 100f);

        var newMovingChance = 50f;
        var newRotateChance = 100f;

        if (typeChance < newMovingChance)
            trapPrefab = MovingTrapsPrefabs[Random.Range(0, MovingTrapsPrefabs.Length)];
        else if (typeChance < newRotateChance)
            trapPrefab = RotatingTrapsPrefabs[Random.Range(0, RotatingTrapsPrefabs.Length)];
        else trapPrefab = StandingTrapsPrefabs[Random.Range(0, StandingTrapsPrefabs.Length)];

        var spawnedPlatform = Instantiate(trapPrefab, new Vector3(Random.Range(-horizontalLimit, horizontalLimit), yDistance, 0f), Quaternion.identity, transform);
        //spawnedPlatform.transform.parent = this.transform;
    }

    void SpawnPlatform(float yDistance)
    {
        GameObject platformPrefab;
        int typeChance = Random.Range(0, 101);

        //float newMovingChance = Mathf.Clamp((movingChance + GameController.distanceTraveled / 100f), 10f, 50f);
        //float newRotateChance = Mathf.Clamp((rotatingChance + GameController.distanceTraveled / 100f), 20f, 100f);

        var newMovingChance = 50f;
        var newRotateChance = 100f;

        if (typeChance < newMovingChance)
            platformPrefab = MovingPlatformPrefabs[Random.Range(0, MovingPlatformPrefabs.Length)];
        else if (typeChance < newRotateChance)
            platformPrefab = RotatingPlatformPrefabs[Random.Range(0, RotatingPlatformPrefabs.Length)];
        else platformPrefab = StandingPlatformPrefabs[Random.Range(0, StandingPlatformPrefabs.Length)];

        var spawnedPlatform = Instantiate(platformPrefab, new Vector3(Random.Range(-horizontalLimit, horizontalLimit), yDistance, 0f), Quaternion.identity, transform);
        //spawnedPlatform.transform.parent = this.transform;
    }

    void Spawn(float yDistance)
    {
        if (!GameController.GameStarted || !spawning)
            return;

        int rndChance = Random.Range(0, 120);

        if (rndChance <= trapChance)// Trap
            SpawnTrap(yDistance);
        else if (rndChance <= 100) // Platform
            SpawnPlatform(yDistance);
        // else skip spawn
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (player.position.y <= currentDistanceCheck)
            {
                Spawn(currentDistanceCheck - verticalDistanceToSpawnTo);

                currentDistanceCheck -= distanceBetweenSpawnsChecks;
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }
}
