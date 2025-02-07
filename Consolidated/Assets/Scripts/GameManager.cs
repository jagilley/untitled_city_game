﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;
//using System.Random;

//using System;

//using System.Collections;
//using System.Collections.Generic;
 
 
public static class ListExtensions  {
    public static void Shuffle<T>(this IList<T> list) {
        System.Random rnd = new System.Random();
        for (var i = 0; i < list.Count; i++)
            list.Swap(i, rnd.Next(i, list.Count));
    }
 
    public static void Swap<T>(this IList<T> list, int i, int j) {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}

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
    //private Spawner[] startspawners;
    public int activespawners;
    public GameObject Over;
    public GoldManager goldManager;
    public GameObject spawners1;
    public GameObject spawners2;


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
        //startspawners = GameObject.FindGameObjectsWithTag("startspawn");
        activespawners = 3;
        //spawnerscript = spawner.GetComponent<Spawner>();
    }

    public void ExploreUp(){
        activespawners = 14;
        spawners1.SetActive(true);
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
        if (goldManager.health < 1) {
            Time.timeScale = 0;
            Over.SetActive(true);
        }

        if (clock > 2400){
            clock = 0;
            day++;
        }
        if (timer < 0){
            if (state == 0){
                if (activespawners == 14){
                    //print("here1");
                    state = 1;
                    timer = 48;
                    //spawners.Shuffle();
                    foreach (Transform child in spawners2.transform){
                        child.GetComponent<Spawner>().state = 1;
                    }
                    foreach (Transform child in spawners1.transform){
                        child.GetComponent<Spawner>().state = 1;
                    }
                }
                else {
                    state = 1;
                    timer = 48;
                    int spns = 1;
                    if (day < 2){
                        spns = 1;
                    }
                    else if (day < 4){
                        spns = 2;
                    }
                    else{
                        spns = 4;
                    }
                    spawners.Shuffle();
                    for (int i = 0; i < spns; i++){
                        
                        Spawner tmp1 = spawners[i];
                        tmp1.state = 1;
                    }
                }
                
            }
            else {
                state = 0;
                //print("here2");
                timer = 48;
                //spawner.SetActive(false);
                foreach (Transform child in spawners2.transform){
                    child.GetComponent<Spawner>().state = 0;
                }
                foreach (Transform child in spawners1.transform){
                    child.GetComponent<Spawner>().state = 0;
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
