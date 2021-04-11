using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Spawn Random Rotation Properties")]
    public bool spawnRandomRotation = false;
    public float spawnRotatedChance = 30f;
    public float spawnRotateLimit = 20f;

    [Header("Horizontal Movement Properties")]
    public bool moveHorizontal = false;
    public float baseHorizontalSpeed = 0.3f;
    public float horizontalRandom = 0.1f;

    [Header("Rotation Properties")]
    public bool rotate = false;
    public float rotateBaseSpeed = 3f;
    public float rotateRandomSpeed = 1f;
    public float rotateLimit = 40f;
    public float rotateRandomLimit = 20f;
    public bool endlessRotation = false;

    [Header("Light Properties")]
    public GameObject lightPrefab;

    private Rigidbody2D rb2D;

    private float finalHorizontalSpeed = 0f;

    private bool lighted = false;

    private float horizontalLimit;

    private float currentRotateSpeed;

    void Start()
    {
        horizontalLimit = Random.Range(1f, 8f);

        rb2D = gameObject.GetComponent<Rigidbody2D>();

        if (spawnRandomRotation)
            RandomizeInitialRotation();

        if (moveHorizontal)
            RandomizeHorizontalSpeed();

        if(rotate)
        {
            currentRotateSpeed = rotateBaseSpeed + Random.Range(-rotateRandomSpeed, rotateRandomSpeed);
            rotateLimit += Random.Range(-rotateRandomLimit, rotateRandomLimit);
        }
    }

    void RandomizeHorizontalSpeed()
    {
        float rnd = Random.Range(-horizontalRandom, horizontalRandom);

        finalHorizontalSpeed = baseHorizontalSpeed + rnd;
    }

    void RandomizeInitialRotation()
    {
        if (!moveHorizontal && !rotate)
        {
            // Randomize initial rotation chance
            float spawnRotate = Random.Range(0, 100);
            if (spawnRotate <= spawnRotatedChance)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-spawnRotateLimit, spawnRotateLimit)));
            }
        }
    }

    void FixedUpdate()
    {
        if (moveHorizontal)
        {
            //rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
            float pingPong = Mathf.PingPong(Time.time * finalHorizontalSpeed, 1);
            
            rb2D.MovePosition(Vector3.Lerp(new Vector3(-horizontalLimit, transform.position.y, 0), new Vector3(horizontalLimit, transform.position.y, 0), pingPong));
        }
        
        if(rotate)
        {
            if(endlessRotation)
            {
                rb2D.MoveRotation(rb2D.rotation + currentRotateSpeed * Time.fixedDeltaTime);
            }
            else
            {
                float pingPong = Mathf.PingPong(Time.time * currentRotateSpeed * Time.fixedDeltaTime, 1);

                rb2D.MoveRotation(Vector3.Lerp(new Vector3(0f, 0f, -rotateLimit), new Vector3(0f, 0f, rotateLimit), pingPong).z);
            }
        }
    }

    public void LightPlatform()
    {
        if (lighted)
            return;

        lighted = true;

        Instantiate(lightPrefab, this.transform);
    }
}
