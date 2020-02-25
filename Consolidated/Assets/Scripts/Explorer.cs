using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{
    GameManager gm;
    private int awake;
    Building building;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        awake = 0;
        building = gameObject.GetComponent<Building>();
    }

    // Update is called once per frame
    void Update()
    {
        if (awake == 0){
            if (building.alive){
                awake = 1;
                gm.ExploreUp();
            }
        }
    }
}
