using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour
{
    bool raising = true;

    public float torchRandomOffset = 2.5f;

    public float backgroundOffset = 10.5f;

    private float liftSpeed;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeTorchPosition();
        liftSpeed = PlatformMovement.liftSpeed;
    }

    void RandomizeTorchPosition()
    {
        for(int i = 0; i<transform.childCount; i++)
        {
            Vector2 rndPosition = Random.insideUnitCircle * torchRandomOffset;

            transform.GetChild(i).transform.position += new Vector3(rndPosition.x, rndPosition.y, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (raising && GameController.GameStarted)
        {
             transform.position = new Vector3(transform.position.x, transform.position.y + liftSpeed, transform.position.z);

            if (transform.position.y >= backgroundOffset)
                ResetPosition();
        }
    }

    void ResetPosition()
    {
        transform.position = new Vector3(transform.position.x, -backgroundOffset, 0f);

        RandomizeTorchPosition();

        GameController.distanceTraveled += backgroundOffset;
    }
}
