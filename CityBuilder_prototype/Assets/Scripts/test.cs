using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private int state;
    private GridManager g;
    private bool awake;
    private Vector3 newmove;
    private GoldManager gold;
    private GameObject building_pf;
    private Transform bh_transform;
    
    // Start is called before the first frame update
    void Start()
    {
        gold = GameObject.FindObjectOfType<GoldManager>();
        g =  gameObject.GetComponent<GridManager>();    
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        b.center = b.center + new Vector3(0,g.h/2,g.w/2);
        b.size = new Vector3(2,g.h + 1,g.w+1);
        state = 0;
        awake = false;
        building_pf = Resources.Load("Building", typeof(GameObject)) as GameObject;
        bh_transform = FindObjectOfType<Building_Holder>().gameObject.transform;
    }

    bool Occupied(Vector3 grid_pos){
        if (g.taken[(int)grid_pos.x*g.w + (int)grid_pos.z] == 0){
            return(false);
        }
        else {
            return(true);
        }
    }

    void Occupy(Vector3 grid_pos){
        g.taken[(int)grid_pos.x*g.w + (int)grid_pos.z] = 1;
    }

    // rn this only works if the grid itself is located on integer coordinates
    Vector3 Gridize(Vector3 vec){
        int x = 0;
        int y = 0;
              
        x = Mathf.RoundToInt(vec.x);
        y = Mathf.RoundToInt(vec.z);
        
        if (x >= g.w + (int) g.transform.position.x) {
            x = g.w - 1 + (int) g.transform.position.x;
            //print(x);
        }
        if (x <= 0 + (int) g.transform.position.x) {
            x = 0 + (int) g.transform.position.x;
        }
        if (y >= g.h + (int) g.transform.position.z){
            y = g.h - 1 + (int) g.transform.position.z;
            //print(y);
        }
        if (y <= 0 + (int) g.transform.position.z){
            y = 0 + (int) g.transform.position.z;
        }

        
        return new Vector3(x,0,y);
    }



    void OnMouseDown(){
        if (awake == false){
            awake = true;
            print("woke up");
        }
        else {
            awake = false;
            print("sleep");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (awake == true){
            
            if (state == 0){
                if (gold.balance >= gold.build_cost){
                    if (Input.GetKeyDown(KeyCode.Space)){
                    //GameObject hello = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    GameObject hello = GameObject.Instantiate(building_pf, bh_transform);
                    hello.transform.position = new Vector3(0,0,0);
                    hello.AddComponent<AudioSource>();
                    newmove = g.transform.position;
                    state = 2;
                    }   
                }
            }
            else{
                
                GameObject go = GameObject.FindObjectOfType<AudioSource>().gameObject;
                // code for moving with mouse
                //Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                // code section for moving blocks with arrow keys
                Vector3 mp = newmove;
                if (Input.GetKeyDown("up")){
                    mp = mp + new Vector3(0,0,1);
                }
                if (Input.GetKeyDown("down")){
                    mp = mp - new Vector3(0,0,1);
                }
                if (Input.GetKeyDown("left")){
                    mp = mp - new Vector3(1,0,0);
                }
                if (Input.GetKeyDown("right")){
                    mp = mp + new Vector3(1,0,0);
                }
                newmove = Gridize(mp);


                go.transform.position = Gridize(new Vector3(mp.x, 0, mp.z));
                if (Input.GetKeyDown(KeyCode.Space)){
                    Vector3 grid_pos = go.transform.position - g.transform.position;

                    if (!Occupied(grid_pos)){
                        Occupy(grid_pos);
                        state = 0;
                        awake = false;

                        //update gold generation
                        gold.gen_buildings++;
                        gold.balance -= gold.build_cost;

                    }
                }
            }
        }

    }
}
