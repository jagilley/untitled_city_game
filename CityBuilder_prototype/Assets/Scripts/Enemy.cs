using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
<<<<<<< HEAD
    private float speed = 0.01f;
    // Pull destinations from global waypoints array
    private float dest_x = 0f;
    private float dest_y = 0f;
=======
    private float speed = 0.05f;
>>>>>>> 711d5e49af469ce061ba97343ef1560d9c22cb45

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        transform.Translate(dest_x, dest_y, speed);
        if(transform.position.z > 30)
=======
        transform.Translate(0f,0f, speed);
        if(transform.position.z > 10)
>>>>>>> 711d5e49af469ce061ba97343ef1560d9c22cb45
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }
    }
}
