using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesFollow : MonoBehaviour
{
    public Transform player;

    void OnTriggerExit2D(Collider2D col)
    {
        //if(col.gameObject.CompareTag("Environment") || col.gameObject.CompareTag("Platform"))
            Destroy(col.gameObject);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        else
        {
            var playerPos = player.position;
            this.transform.position = new Vector3(0f, playerPos.y, 0f);
        }
    }
}
