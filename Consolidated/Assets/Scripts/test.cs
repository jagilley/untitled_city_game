﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public int state;
    private GridManager g;
    public bool awake;
    private Vector3 newmove;
    private GoldManager gold;
    private GameObject building_pf;
    private GameObject explore_pf;
    private GameObject missile_pf;
    private GameObject slow_pf;
    private Transform bh_transform;
    public GameObject cam;
    public GameObject blocker;
    private GameObject selector;
    private Material highlight;
    private Material normal;
    public GameObject passiveshop;
    public TurretShop turrshop;

    // Start is called before the first frame update
    void Start()
    {
        //initialize literally everything        
        highlight = Resources.Load("Outline", typeof(Material)) as Material;

        //gold manager
        gold = GameObject.FindObjectOfType<GoldManager>();
        g = gameObject.GetComponent<GridManager>();
        //box collider for the grid to know when its clicked on        
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        b.center = b.center + new Vector3(0, g.h / 2f, g.w / 2f);
        //b.center = highlighter.transform.position;
        b.size = new Vector3(1, g.h + 1, g.w + 1);
        //b.size = */
        //track when grid is awake or asleep
        state = 0;
        awake = false;
        //passive building prefab and holder 
        building_pf = Resources.Load("Building", typeof(GameObject)) as GameObject;
        explore_pf = Resources.Load("Explorer", typeof(GameObject)) as GameObject;
        missile_pf = Resources.Load("missileresearch", typeof(GameObject)) as GameObject;
        slow_pf = Resources.Load("slowresearch", typeof(GameObject)) as GameObject;
        bh_transform = FindObjectOfType<Building_Holder>().gameObject.transform;
        cam = GameObject.FindObjectOfType<CameraController>().gameObject;
        //blocker = GameObject.FindObjectOfType<Image>().gameObject;
        //blocker.SetActive(false);
        selector = Resources.Load("Selector", typeof(GameObject)) as GameObject;
        normal = Resources.Load("Normal", typeof(Material)) as Material;
    }

    //function to find world positions
    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z){
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.up, new Vector3(0,0,z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    //this checks if the current grid space already has a building on it
    bool Occupied(Vector3 grid_pos)
    {
        if (g.taken[(int)grid_pos.x * g.w + (int)grid_pos.z] == 0)
        {
            return (false);
        }
        else
        {
            return (true);
        }
    }

    //call this to occupy the current grid space
    void Occupy(Vector3 grid_pos)
    {
        g.taken[(int)grid_pos.x * g.w + (int)grid_pos.z] = 1;
        print((int)grid_pos.x * g.w + (int)grid_pos.z);
    }

    public void UnOccupy(Vector3 grid_pos)
    {
        g.taken[(int)grid_pos.x * g.w + (int)grid_pos.z] = 0;
    }

    // rn this only works if the grid itself is located on integer coordinates
    // assuming grid spots are integer spaces apart, so each move is an integer move
    Vector3 Gridize(Vector3 vec)
    {
        int x = 0;
        int y = 0;

        x = Mathf.RoundToInt(vec.x);
        y = Mathf.RoundToInt(vec.z);

        if (x >= g.w + (int)g.transform.position.x)
        {
            x = g.w - 1 + (int)g.transform.position.x;
            //print(x);
        }
        if (x <= 0 + (int)g.transform.position.x)
        {
            x = 0 + (int)g.transform.position.x;
        }
        if (y >= g.h + (int)g.transform.position.z)
        {
            y = g.h - 1 + (int)g.transform.position.z;
            //print(y);
        }
        if (y <= 0 + (int)g.transform.position.z)
        {
            y = 0 + (int)g.transform.position.z;
        }


        return new Vector3(x, 0, y);
    }


    // wake up this specific grid when this grid is clicked on 
    void OnMouseDown()
    {
        if (awake == false && gold.balance >= gold.build_cost)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                awake = true;
                passiveshop.SetActive(true);
                GameObject highlighter = g.gameObject.transform.GetChild(g.gameObject.transform.childCount - 1).gameObject;
                Color c = Color.white;
                c.a = .2f;
                highlighter.GetComponent<Renderer>().material.color = c;
                print("woke up");
            }
        }
        else
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                awake = false;
                print("sleep");
            }
        }
    }

    void switch1(GameObject go, Vector3 currpos){
        if (gold.balance > gold.build_cost){
            GameObject.Destroy(go);
            GameObject hello = GameObject.Instantiate(building_pf,currpos,Quaternion.identity, bh_transform);
            hello.AddComponent<AudioSource>();
            hello.GetComponent<Building>().parent_grid = gameObject;
            newmove = g.transform.position;
            state = 1;}
    }
    void switch2(GameObject go, Vector3 currpos){
        if (gold.balance > gold.explore_cost){
            GameObject.Destroy(go);
            GameObject hello = GameObject.Instantiate(explore_pf,currpos,Quaternion.identity, bh_transform);
            hello.AddComponent<AudioSource>();
            hello.GetComponent<Building>().parent_grid = gameObject;
            newmove = g.transform.position;
            state = 2;
        }
    }
    void switch3(GameObject go, Vector3 currpos){
        if (gold.balance > gold.research_cost){
            GameObject.Destroy(go);
            GameObject hello = GameObject.Instantiate(missile_pf,currpos,Quaternion.identity, bh_transform);
            hello.AddComponent<AudioSource>();
            hello.GetComponent<Building>().parent_grid = gameObject;
            newmove = g.transform.position;
            state = 3;}
    }
    void switch4(GameObject go, Vector3 currpos){
        if (gold.balance > gold.research_cost){
            GameObject.Destroy(go);
            GameObject hello = GameObject.Instantiate(slow_pf,currpos,Quaternion.identity, bh_transform);
            hello.AddComponent<AudioSource>();
            hello.GetComponent<Building>().parent_grid = gameObject;
            newmove = g.transform.position;
            state = 4;}
    }
    
    
    // Update is called once per frame
    void Update()
    {

        if (awake == true)
        {
            blocker.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape)){
                awake = false;
                blocker.SetActive(false);
                GameObject highlighter = g.gameObject.transform.GetChild(g.gameObject.transform.childCount - 1).gameObject;
                Color c = Color.white;
                c.a = 0f;
                highlighter.GetComponent<Renderer>().material.color = c;
            }
            if (state == 0)
            {
                // press 1 key to spawn a passive building
                if (Input.GetKeyDown(KeyCode.Alpha1)) {
                    if (gold.balance > gold.build_cost)
                    {
                        Vector3 mousepos = Input.mousePosition;
                        mousepos.z = cam.transform.position.z;
                        Vector3 mp = GetWorldPositionOnPlane(mousepos, 0f);
                        Vector3 currpos = Gridize(new Vector3(mp.x, 0, mp.z));
                        
                        GameObject hello = GameObject.Instantiate(building_pf,currpos, Quaternion.identity, bh_transform);
                        
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 1;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha2)) {
                    if (gold.balance > gold.explore_cost)
                    {
                        Vector3 mousepos = Input.mousePosition;
                        mousepos.z = cam.transform.position.z;
                        Vector3 mp = GetWorldPositionOnPlane(mousepos, 0f);
                        Vector3 currpos = Gridize(new Vector3(mp.x, 0, mp.z));
                                    
                        GameObject hello = GameObject.Instantiate(explore_pf,currpos, Quaternion.identity, bh_transform);
                        
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 2;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha3)) {
                    if (gold.balance > gold.research_cost)
                    {
                        Vector3 mousepos = Input.mousePosition;
                        mousepos.z = cam.transform.position.z;
                        Vector3 mp = GetWorldPositionOnPlane(mousepos, 0f);
                        Vector3 currpos = Gridize(new Vector3(mp.x, 0, mp.z));
                                    
                        GameObject hello = GameObject.Instantiate(missile_pf,currpos, Quaternion.identity, bh_transform);
                        
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 3;
                        
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha4)) {
                    if (gold.balance > gold.research_cost)
                    {
                        Vector3 mousepos = Input.mousePosition;
                        mousepos.z = cam.transform.position.z;
                        Vector3 mp = GetWorldPositionOnPlane(mousepos, 0f);
                        Vector3 currpos = Gridize(new Vector3(mp.x, 0, mp.z));
                                    
                        GameObject hello = GameObject.Instantiate(slow_pf,currpos, Quaternion.identity, bh_transform);
                        
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 4;
                        
                    }
                }
            }
            else
            {

                GameObject go = GameObject.FindObjectOfType<AudioSource>().gameObject;
                // code for moving with mouse
                Vector3 mousepos = Input.mousePosition;
                mousepos.z = cam.transform.position.z;
                Vector3 mp = GetWorldPositionOnPlane(mousepos, 0f);

                if (Input.GetKeyDown(KeyCode.Escape)){
                    
                    state = 0;
                    awake = false;
                    blocker.SetActive(false);
                    GameObject highlighter = g.gameObject.transform.GetChild(g.gameObject.transform.childCount - 1).gameObject;
                    Color c = Color.white;
                    c.a = 0f;
                    highlighter.GetComponent<Renderer>().material.color = c;
                    Object.Destroy(go);
                }
                Vector3 currpos = Gridize(new Vector3(mp.x, 0, mp.z));

                if (state == 1){
                    if (Input.GetKeyDown(KeyCode.Alpha2)){
                        switch2(go, currpos);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3)){
                        switch3(go, currpos);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha4)){
                        switch4(go, currpos);
                    }
                }

                if (state == 2){
                    if (Input.GetKeyDown(KeyCode.Alpha1)){
                        switch1(go, currpos);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3)){
                        switch3(go, currpos);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha4)){
                        switch4(go, currpos);
                    }
                }

                if (state == 3){
                    if (Input.GetKeyDown(KeyCode.Alpha1)){
                        switch1(go, currpos);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2)){
                        switch2(go, currpos);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha4)){
                        switch4(go, currpos);
                    }
                }

                if (state == 4){
                    if (Input.GetKeyDown(KeyCode.Alpha1)){
                        switch1(go, currpos);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2)){
                        switch2(go, currpos);
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3)){
                        switch3(go, currpos);
                    }
                }

                // code section for moving blocks with arrow keys
                /*Vector3 mp = newmove;
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
*/

                go.transform.position = Gridize(new Vector3(mp.x, 0, mp.z));
                //if (Input.GetKeyDown(KeyCode.Space)){
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 grid_pos = go.transform.position - g.transform.position;

                    if (!Occupied(grid_pos))
                    {
                        Occupy(grid_pos);
                        if (state == 1){
                            //update gold generation
                            gold.gen_buildings++;
                            gold.balance -= gold.build_cost;
                        }

                        if (state == 3){
                            turrshop.missile = true;
                        }
                        if (state == 4){
                            turrshop.slow = true;
                        }

                        state = 0;
                        awake = false;
                        go.GetComponent<Building>().alive = true;
                        go.GetComponent<Renderer>().material = normal;
                        
                        blocker.SetActive(false);
                        GameObject highlighter = g.gameObject.transform.GetChild(g.gameObject.transform.childCount - 1).gameObject;
                        Color c = Color.white;
                        c.a = 0f;
                        highlighter.GetComponent<Renderer>().material.color = c;

                    }
                }
            }
        }

    }
}
