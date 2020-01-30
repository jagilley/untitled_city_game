using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private int state;
    private GridManager g;
    private bool awake;
    
    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space)){
            GameObject hello = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Grid grid = gameObject.GetComponent<Grid>();
            Vector3 pos = new Vector3(5,7,0);
            hello.transform.position = grid.CellToWorld(grid.LocalToCell(pos));
        }*/

        if (awake == true){

            if (state == 0){
                if (Input.GetKeyDown(KeyCode.Space)){
                GameObject hello = GameObject.CreatePrimitive(PrimitiveType.Cube);
                hello.transform.position = new Vector3(0,0,0);
                hello.AddComponent<AudioSource>();
                state = 2;
                }
            }

            //if (state == 2) {
            else{
                GameObject go = GameObject.FindObjectOfType<AudioSource>().gameObject;
                Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                go.transform.position = Gridize(new Vector3(mp.x, mp.y, 0));
                if (Input.GetKeyDown(KeyCode.Space)){
                    if (!Occupied(go.transform.position)){
                        Occupy(go.transform.position);
                        state = 0;
                        awake = false;
                    }
                }
            }
        }

    }
}
