using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int balance;
    public int health;
    public int gen_buildings;
    private int gen_rate;
    public int build_cost;
    public int turr_cost;
    public int explore_cost;
    public int research_cost;

    // Start is called before the first frame update
    void Start()
    {
        balance = 200;
        health = 100;
        gen_buildings = 0;
        gen_rate = 5;
        build_cost = 100;
        turr_cost = 50;
        explore_cost = 1000;
        research_cost = 150;
        StartCoroutine( GoldUpdater() );
    }

    IEnumerator GoldUpdater(){
        yield return new WaitForSeconds(1f);
        while (true) {
            balance = balance + gen_buildings * gen_rate;    
            yield return new WaitForSeconds(1f);
        }
    }

    public void addGold(float cost)
    {
        balance += turr_cost / 2;
    }

    public void spendUpgrade(int upgradeC)
    {
        balance -= upgradeC;
    }
/*
    private void goldupdate(){
        balance = balance + gen_buildings * gen_rate;
        print("called");
    } */


    // Update is called once per frame
    void Update()
    {
        //InvokeRepeating("goldupdate",5f,5f);
    }
}
