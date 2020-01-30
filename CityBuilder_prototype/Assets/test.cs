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
    
    // Start is called before the first frame update
    void Start()
    {
        gold = GameObject.FindObjectOfType<GoldManager>();
        g =  gameObject.GetComponent<GridManager>();    
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        b.center = b.center + new Vector3(g.w/2,g.h/2);
        //b.center = new Vector3(g.w/2+g.transform.position.x, g.h/2+g.transform.position.y,0);
        b.size = new Vector3(g.w + 1,g.h+1,0);
        state = 0;
        awake = false;
    }

    bool Occupied(Vector3 grid_pos){
        if (g.taken[(int)grid_pos.x*g.w + (int)grid_pos.y] == 0){
            return(false);
        }
        else {
            return(true);
        }
    }

    void Occupy(Vector3 grid_pos){
        g.taken[(int)grid_pos.x*g.w + (int)grid_pos.y] = 1;
    }

    // rn this only works if the grid itself is located on integer coordinates
    Vector3 Gridize(Vector3 vec){
        int x = 0;
        int y = 0;
              
        x = Mathf.RoundToInt(vec.x);
        y = Mathf.RoundToInt(vec.y);
        
        if (x >= g.w + (int) g.transform.position.x) {
            x = g.w - 1 + (int) g.transform.position.x;
            //print(x);
        }
        if (x <= 0 + (int) g.transform.position.x) {
            x = 0 + (int) g.transform.position.x;
        }
        if (y >= g.h + (int) g.transform.position.y){
            y = g.h - 1 + (int) g.transform.position.y;
            //print(y);
        }
        if (y <= 0 + (int) g.transform.position.y){
            y = 0 + (int) g.transform.position.y;
        }

        
        return new Vector3(x,y,0);
    }



    void OnMouseDown(){
        awake = true;
        print(g.taken);
    }

    // Update is called once per frame
    void Update()
    {

        if (awake == true){
            
            if (state == 0){
                if (gold.balance >= gold.build_cost){
                    if (Input.GetKeyDown(KeyCode.Space)){
                    GameObject hello = GameObject.CreatePrimitive(PrimitiveType.Cube);
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
                    mp = mp + new Vector3(0,1,0);
                }
                if (Input.GetKeyDown("down")){
                    mp = mp - new Vector3(0,1,0);
                }
                if (Input.GetKeyDown("left")){
                    mp = mp - new Vector3(1,0,0);
                }
                if (Input.GetKeyDown("right")){
                    mp = mp + new Vector3(1,0,0);
                }
                newmove = mp;


                go.transform.position = Gridize(new Vector3(mp.x, mp.y, 0));
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
