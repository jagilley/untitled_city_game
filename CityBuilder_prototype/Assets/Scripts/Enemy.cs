using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 0.01f;
    // Pull destinations from global waypoints array
    private float dest_x = 0f;
    private float dest_y = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dest_x, dest_y, speed);
        if(transform.position.z > 30)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -50f);
        }
    }
}
