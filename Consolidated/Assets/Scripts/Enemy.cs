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

    public float health = 100f;
    //private float speed = 0.05f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-0.01f,0, -0.01f);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        //transform.Translate(dest_x, dest_y, speed);
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0.01f);
        //if(transform.position.z > 30)
        //transform.Translate(0f,0f, speed);
        /*
        if(transform.position.z > 10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health -= 20;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
