using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 0.15f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f,0f, speed);
        if(transform.position.z > 30)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -50f);
        }
    }
}
