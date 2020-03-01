using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Holder : MonoBehaviour
{
    
    private Transform trans;
    private GameObject last;
    public int num_active;
    public int num_build;
    private bool on;
    private GoldManager g;

    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.transform;
        num_active = 0;
        num_build = 0;
        last = gameObject;
        on = false;
        g = Object.FindObjectOfType<GoldManager>();
    }

    public void Destroy_Active(){
        if (last.tag == "GoldMine")
        {
            g.addGold(g.build_cost);
        }
        else if(last.tag == "slowresearch" || last.tag == "missileresearch")
        {
            g.addGold(g.research_cost);
        }
        else
        {
            g.addGold(g.turr_cost);
        }
        last.GetComponent<Building>().UnOccupy();
        Destroy(last);
    }


    // Update is called once per frame
    void LateUpdate()
    {
        on = false;
        num_build = trans.childCount;
        foreach (Transform child in transform){
            GameObject curr = child.gameObject;
            //int currnum = curr.GetComponent<Building>().selfnum;
            if (curr.GetComponent<Building>().awake){
                on = true;
                if (Object.ReferenceEquals(curr, last)){
                        //print("here");
                    }
                
                
                else{
                    print("here2");
                    num_active++;
                    if (num_active == 1){
                        last = curr;
                        print("here3");
                    }
                    else if (num_active == 2) {
                        print("here4");
                        last.GetComponent<Building>().awake = false;
                        last = curr;
                        num_active = 1;
                    }
                    else {
                        print("here5");
                        num_active = 0;
                        foreach (Transform cchild in transform){
                            cchild.gameObject.GetComponent<Building>().awake = false;
                        }
                    }
                }
            }
        }
        if (on == false){
            num_active = 0;
        }


    }
}
