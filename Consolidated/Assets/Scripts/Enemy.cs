using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f;
    int cyl_health=cylinder.cyl_health;
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
    public Transform BuildGrid;
    public GameObject bg;

    void Start()
    {
        //door_health = GameObject.Find("dh2");
        waypoints = GameObject.FindGameObjectsWithTag("Waypoints");
        bg = GameObject.Find("BuildGrid");
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

        if(Vector3.Distance(bg.transform.position,transform.position) <= 4)
        {
            //door_health.sizeDelta = new Vector2(door_health.sizeDelta.x-1, door_health.sizeDelta.y);
            //door_health.sizeDelta = new Vector3(door_health.sizeDelta.x-1, door_health.sizeDelta.y, door_health.sizeDelta.z);
            //Debug.Log(cyl_health);
            cylinder.cyl_health = cylinder.cyl_health - 1;

            Destroy(gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    void OnTriggerEnter(Collider other)
    { 
        if(Vector3.Distance(BuildGrid.position,transform.position) < 3)
        {
            //door_health.sizeDelta = new Vector2(door_health.sizeDelta.x-1, door_health.sizeDelta.y);
            //door_health.sizeDelta = new Vector3(door_health.sizeDelta.x-1, door_health.sizeDelta.y, door_health.sizeDelta.z);
            //Debug.Log(cyl_health);
            cylinder.cyl_health = cylinder.cyl_health - 1;
            Debug.Log(cylinder.cyl_health);
            Destroy(gameObject);
        }
    }

    int CompareWaypoints(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }
}
