using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f;
    // Pull destinations from global waypoints array
    private float dest_x = 0f;
    private float dest_y = 0f;

    public float health = 100f;
    //private float speed = 0.05f;
    public GameObject[] waypoints;
    public Vector3[] vectors;
    private int currentWaypoint = 0;
    private float lastWaypointSwitchTime;
    private GameObject wp1;
    private GameObject wp2;
    int current = 0;
    float rotSpeed;
    float WPradius = 0.25f;


    void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoints");
        Array.Sort(waypoints, CompareWaypoints);
        Array.Reverse(waypoints);
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            current++;
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    void OnTriggerEnter(Collider other)
    { 
        if(other.gameObject.tag == "door")
        {
            Destroy(gameObject);
        }
    }

    int CompareWaypoints(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }
}
