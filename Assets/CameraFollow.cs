using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float camspeed;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null) 
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, 0, player.position.z) + offset, Time.deltaTime * camspeed);
        }
    }
}
