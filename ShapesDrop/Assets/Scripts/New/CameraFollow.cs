using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float horizontalLimit = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        else
        {
            var playerPos = player.position;
            this.transform.position = new Vector3(Mathf.Clamp(playerPos.x, -horizontalLimit, horizontalLimit), playerPos.y, -10f);
        }
    }
}
