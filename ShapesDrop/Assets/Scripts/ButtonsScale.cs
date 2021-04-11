using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScale : MonoBehaviour
{
    float scalingTime, scalingSpeed = 0.15f, targetScale = 0.9f;
    float length = 0.1f;

    void Update()
    {
        scalingTime = Time.time * scalingSpeed;
        transform.localScale = new Vector3(
            Mathf.PingPong(scalingTime, length) + targetScale,
            Mathf.PingPong(scalingTime, length) + targetScale, 0
        );
    }
}
