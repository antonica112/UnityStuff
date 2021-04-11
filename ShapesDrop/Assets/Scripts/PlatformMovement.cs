using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    // Lift
    public static float liftSpeed = 0.020f;
    private bool raising = true;

    // Light
    private bool lighted = false;

    public GameObject lightPrefab;

    public bool spawnLight = false;

    public int lightChange = 30;

    // Shake
    bool shaking = false;

    public bool canShake = true;

    public float shakeOffset = 0.02f;

    public float shakeDuration = 0.2f;

    // Horizontal Movement
    public bool moveHorizontal = false;

    public float horizontalSpeed = 0.2f;

    public float horizontalRandom = 0.2f;

    // Rotation
    public bool rotate  = false;
    public bool endlessRotate = false;
    public float endlessRotateChance = 30f;
    public Vector3 rotateFrom = new Vector3(0f, 0f, -20f);
    public Vector3 rotateTo = new Vector3(0f, 0f, 20f);
    public float rotateSpeed = 1f;
    public float rotateRandomSpeed = 1.5f;
    public bool spawnRotated = true;
    public float spawnRotatedChance = 30f;
    public float rotateSpawnLimit = 20f;
    private bool rotateRight = true;

    public bool spawnRandomRotation = true;

    private float startTime;
    private float journeyLength;

    Vector3 originalPosition;

    private float finalHorizontalSpeed = 1f;

    void Start()
    {
        if(spawnRandomRotation)
            RandomizeInitialRotation();

        if (moveHorizontal)
            RandomizeHorizontalSpeed();

        if (rotate)
        {
            rotateSpeed += Random.Range(-rotateRandomSpeed, rotateRandomSpeed);

            // Randomize between limited rotation & endless
            int rotateRnd = Random.Range(0, 100);
            if (rotateRnd <= endlessRotateChance)
            {
                // Endless Rotation
                endlessRotate = true;

                rotateRnd = Random.Range(1, 2);
                if (rotateRnd == 1)
                    rotateRight = true;
                else rotateRight = false;
            }
            else
            {
                // Limited rotation
                endlessRotate = false;
            }
        }

        if (spawnLight)
        {
            int rnd = Random.Range(1, 101);

            if (rnd <= lightChange)
            {
                LightPlatform();
            }
        }
    }

    void RandomizeInitialRotation()
    {
        if (!moveHorizontal && !rotate)
        {
            // Randomize initial rotation chance
            float spawnRotate = Random.Range(0, 100);
            if (spawnRotate <= spawnRotatedChance)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(-rotateSpawnLimit, rotateSpawnLimit)));
            }
        }
    }

    public void StopShake()
    {
        if (!canShake)
            return;

        transform.position = originalPosition;
        shaking = false;
    }

    public void ShakePlatform()
    {
        originalPosition = transform.position;
        shaking = true;
    }

    public void LightPlatform()
    {
        if (lighted)
            return;

        lighted = true;

        if (canShake)
        {
            ShakePlatform();
            Invoke("StopShake", shakeDuration);
        }

        Instantiate(lightPrefab, this.transform);
    }

    void RandomizeHorizontalSpeed()
    {
        float rnd = Random.Range(-horizontalRandom, horizontalRandom);

        finalHorizontalSpeed = horizontalSpeed + rnd;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.GameStarted)
            return;

        if (shaking)
        {
            Vector3 tempPos = Vector3.zero;

            tempPos = transform.position;

            float offSet = Random.Range(-shakeOffset, shakeOffset);

            tempPos.x = originalPosition.x + offSet;

            transform.position = tempPos;
        }
        else
        {
            if(moveHorizontal)
            {
                float pingPong = Mathf.PingPong(Time.time * finalHorizontalSpeed, 1);
                transform.position = Vector3.Lerp(new Vector3(-4f, transform.position.y, 0), new Vector3(4f, transform.position.y, 0), pingPong);
            }

            if (raising)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + liftSpeed, transform.position.z);

                if (transform.position.y >= 7f)
                    Destroy(gameObject);
            }

            if (rotate)
            {
                if (endlessRotate)
                {
                    if(rotateRight)
                        transform.Rotate(0f, 0f, this.rotateSpeed);
                    else transform.Rotate(0f, 0f, -this.rotateSpeed);
                }
                else
                {
                    Quaternion from = Quaternion.Euler(this.rotateFrom);
                    Quaternion to = Quaternion.Euler(this.rotateTo);

                    float lerp = 0.5F * (1.0F + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * this.rotateSpeed));
                    this.transform.localRotation = Quaternion.Lerp(from, to, lerp);
                }
            }
        }
    }
}
