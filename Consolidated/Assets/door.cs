using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class door : MonoBehaviour
{
    public RectTransform door_health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter (Collision collision){
        if (collision.gameObject.name == "Enemy"){
            Debug.Log("hit!");
            door_health.sizeDelta = new Vector2(door_health.sizeDelta.x-1, door_health.sizeDelta.y);
        }

    }
}
