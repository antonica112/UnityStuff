using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public bool randomizeInitialPosition = false;

    public float flickerSpeed = 0.3f; // in seconds

    public float flickerRange = 1.5f;

    //private Light2D currentLight;

    private float defaultRadius = 8f;

    float GetCurrentDefaultRadius()
    {
        //float currRadius = Mathf.Clamp((defaultRadius - GameController.distanceTraveled/100f), 0, defaultRadius);

        return defaultRadius;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(randomizeInitialPosition)
            RandomizeInitialPosition();

        /*currentLight = GetComponent<Light2D>();

        defaultRadius = currentLight.pointLightOuterRadius;

        if (currentLight != null)
        {
            InvokeRepeating("FlickerLight", flickerSpeed, flickerSpeed);
        }
        else Debug.LogWarning("Light2D component (LWRP) not found on the torches.");*/
    }

    void RandomizeInitialPosition()
    {
        transform.position = new Vector3(Random.Range(-6f, 6f), transform.position.y, 0f);
    }

    void FlickerLight()
    {
        /*if(currentLight != null)
        {
            float rnd = Random.Range(-flickerRange, flickerRange);

            float newRadius = GetCurrentDefaultRadius() + rnd;

            currentLight.pointLightOuterRadius = newRadius;
        }*/
    }
}
