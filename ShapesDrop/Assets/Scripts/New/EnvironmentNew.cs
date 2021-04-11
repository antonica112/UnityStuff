using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentNew : MonoBehaviour
{
    public GameObject screenBackground;

    public float screenSize = 15f;

    public Transform player;

    private float screenLimit = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void SpawnBackground()
    {
        Instantiate(screenBackground, new Vector3(0f, screenLimit, 0f), Quaternion.identity, this.transform);
    }

    private void LateUpdate()
    {
        if(player)
        {
            if (player.position.y <= screenLimit)
            {
                screenLimit -= screenSize;

                SpawnBackground();
                // Spawn background
            }
        }
    }
}
