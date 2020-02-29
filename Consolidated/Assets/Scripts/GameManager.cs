using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

public class GameManager : MonoBehaviour
{
    public int state;
    //public GameObject spawner;
    public float timer;
    public GameObject wavetimer;
    private Spawner spawnerscript;
    private float clock;
    private int day;
    public GameObject daytimer;
    private Spawner[] spawners;
    public int activespawners;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        // the first day lasts for 60 seconds (15 hours); so the game starts at 3am
        timer = 64;
        // 64
        clock = 300;
        day = 0;
        spawners = GameObject.FindObjectsOfType<Spawner>();
        activespawners = 2;
        //spawnerscript = spawner.GetComponent<Spawner>();
    }

    public void ExploreUp(){
        activespawners = 6;
        CameraController cam = FindObjectOfType<CameraController>();
        if (cam.exploration == 0){
            cam.exploration = 1;
            cam.PanOver();
        }
    }

    /* night starts at 6pm and goes to 6am (12 hrs -> 48 seconds) */

    // Update is called once per frame
    void Update()
    {
        if (clock > 2400){
            clock = 0;
            day++;
        }
        if (timer < 0){
            if (state == 0){
                //print("here1");
                state = 1;
                timer = 48;
                for (int i = 0; i < activespawners; i++){
                    Spawner tmp1 = spawners[i];
                    tmp1.state = 1;
                }
                
            }
            else {
                state = 0;
                //print("here2");
                timer = 48;
                //spawner.SetActive(false);
                for (int i = 0; i < activespawners; i++){
                    Spawner tmp1 = spawners[i];
                    tmp1.state = 0;
                }
            }
        }

        timer -= Time.deltaTime;
        clock += 100 * Time.deltaTime / 4;
        float tmp = Mathf.Round(clock / 100f) * 100f;
        //wavetimer.GetComponent<Text>().text = tmp.ToString("0000");
        daytimer.GetComponent<Text>().text = "Day " + day.ToString() + ", " + tmp.ToString("00:00");
    }
}
