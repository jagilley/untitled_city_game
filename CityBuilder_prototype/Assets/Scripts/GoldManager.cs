using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int balance;
    public int gen_buildings;
    private int gen_rate;
    public int build_cost;
    
    // Start is called before the first frame update
    void Start()
    {
        balance = 200;
        gen_buildings = 0;
        gen_rate = 10;
        build_cost = 100;

        StartCoroutine( GoldUpdater() );
    }

    IEnumerator GoldUpdater(){
        yield return new WaitForSeconds(1f);
        while (true) {
            balance = balance + gen_buildings * gen_rate;    
            yield return new WaitForSeconds(1f);
        }
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
