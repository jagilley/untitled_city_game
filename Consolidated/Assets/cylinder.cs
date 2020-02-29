using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cylinder : MonoBehaviour
{
    public static int cyl_health;
    private GoldManager gold;
    // Start is called before the first frame update
    void Start()
    {
        cyl_health = 100;
        //Debug.Log(cyl_health);
        gold = GameObject.FindObjectOfType<GoldManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gold.health = cyl_health;
    }

    /*
    void OnCollisionEnter (Collision collision){
        if (collision.gameObject.name == "Enemy"){
            Debug.Log("hit!");
            door_health.sizeDelta = new Vector2(door_health.sizeDelta.x-1, door_health.sizeDelta.y);
        }

    }*/
}
