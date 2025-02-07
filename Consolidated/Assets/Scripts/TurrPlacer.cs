﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurrPlacer : MonoBehaviour
{
    public GameObject b1;
    public GameObject b2;
    public GameObject b3;
    public Material cubehighlight;
    public Material cubenolight;
    private Vector3 currpos;
    public int state;
    private GridManager g;
    private bool awake;
    private Vector3 newmove;
    private GoldManager gold;
    public GameObject turr_pf1;
    public GameObject turr_pf2;
    public GameObject turr_pf3;
    private Transform bh_transform;
    private GameObject cam;
    public GameObject blocker;
    public GameObject turretshop;
    public TurretShop tss;
    private nogold ng;
    public GameObject group1;
    public int g1s;
    public GameObject group2;
    public int g2s;
    public GameObject group3;
    public int g3s;
    private int group;
    public GraphicRaycaster graycast;
    PointerEventData ped;
    EventSystem mes;

    // Start is called before the first frame update
    void Start()
    {
        cubehighlight = Resources.Load("Outline", typeof(Material)) as Material;
        cubenolight = Resources.Load("Normal", typeof(Material)) as Material;
        cubehighlight.color = Color.white;
        cubenolight.color = Color.white;
        group = 0;
        //graycast = New GraphicRaycaster();
        mes = GetComponent<EventSystem>();
        //initialize literally everything
        //gold manager
        gold = GameObject.FindObjectOfType<GoldManager>();
        g = gameObject.GetComponent<GridManager>();
        //box collider for the grid to know when its clicked on
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        b.center = b.center + new Vector3(g.w / 2, 0, g.h / 2);
        b.size = new Vector3(g.w + 1, 1, g.h + 1);
        //track when grid is awake or asleep
        state = 0;
        awake = false;
        //turret prefab and holder (holder not yet implemented)
        bh_transform = FindObjectOfType<Building_Holder>().gameObject.transform;
        cam = GameObject.FindObjectOfType<CameraController>().gameObject;
        //this isnt working, so need to set this in the editor manually
        //blocker = GameObject.FindObjectOfType<Blocker>().gameObject;
        //blocker.SetActive(false);
        tss = turretshop.GetComponent<TurretShop>();
        ng = Object.FindObjectOfType<nogold>();
    }


    //this checks if the current grid space already has a building on it
    bool Occupied(Vector3 grid_pos)
    {
        if (g.taken[(int)grid_pos.x * g.h + (int)grid_pos.z] == 0)
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
        g.taken[(int)grid_pos.x * g.h + (int)grid_pos.z] = 1;
    }

    public void UnOccupy(Vector3 grid_pos)
    {
        g.taken[(int)grid_pos.x * g.h + (int)grid_pos.z] = 0;
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

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z){
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.up, new Vector3(0,0,z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    // wake up this specific grid when this grid is clicked on 
    void OnMouseDown()
    {
        /*if (gold.balance < gold.turr_cost){
            ng.Fade();
        }*/
        if (!EventSystem.current.IsPointerOverGameObject() /*&& gold.balance >= gold.turr_cost*/)
        {
            if (awake == false)
            {
                Vector3 mousepos = Input.mousePosition;
                mousepos.z = cam.transform.position.z;
                Vector3 mp = GetWorldPositionOnPlane(mousepos, 0f);
                currpos = Gridize(new Vector3(mp.x, 0, mp.z));
                Vector3 grid_pos = currpos - g.transform.position;
                if (!Occupied(grid_pos)){
                    awake = true;
                    turretshop.SetActive(true);
                    turretshop.GetComponent<CanvasGroup>().alpha = 1;
                    blocker.SetActive(true);
                    
                    GameObject currcube = g.gameObject.transform.GetChild((int)grid_pos.x * g.h + (int)grid_pos.z).gameObject;
                    currcube.GetComponent<Renderer>().material = cubehighlight;


                    GameObject highlighter = g.gameObject.transform.GetChild(g.gameObject.transform.childCount - 1).gameObject;
                    Color c = Color.white;
                    c.a = .2f;
                    highlighter.GetComponent<Renderer>().material.color = c;
                    
                    print("woke up");
                }
            }

            else
            {
                awake = false;
                turretshop.SetActive(false);
                print("sleep");
            }
        }
    }

    void CheckMouseHit(Ray ray, RaycastHit hit, int group){
        print("hi2");
        if (hit.transform.gameObject == group1){
            print("hi3");
            group = 1;
        }
        if (hit.transform.gameObject == group2){
            group = 2;
        }
        if (hit.transform.gameObject == group3){
            group = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Code to be place in a MonoBehaviour with a GraphicRaycaster component
        //GraphicRaycaster gr = graycast;
        //Create the PointerEventData with null for the EventSystem
        //PointerEventData ped = new PointerEventData(null);
        //Set required parameters, in this case, mouse position
        //ped.position = Input.mousePosition;
        //Create list to receive all results
        //List<RaycastResult> results = new List<RaycastResult>();
        //Raycast it
        /*PointerEventData ped = new PointerEventData(null);
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        GraphicRaycaster gr = cam.GetComponent<GraphicRaycaster>();
        gr.Raycast(ped, results);
        if (results.Count > 0){
            print("hi123");
        }*/
        /*RaycastHit hit;
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000f)){
            if (hit.collider.tag == "button"){
                print("hi213");
            }
        }*/
        
        g1s = group1.GetComponent<MouseEnterStore>().state;
        g2s = group2.GetComponent<MouseEnterStore>().state;
        g3s = group3.GetComponent<MouseEnterStore>().state;

        if (awake == true)
        {
            
            /*RaycastHit hit;
            Ray ray;
            if (Input.GetMouseButtonDown(0)){
                print("hi");
                ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit)){
                    CheckMouseHit(ray, hit, group);
                }
                
            }*/

            if (Input.GetKeyDown(KeyCode.Escape)){
                awake = false;
                blocker.SetActive(false);
                /*tss.first.GetComponent<CanvasGroup>().alpha = 0;
                tss.second.GetComponent<CanvasGroup>().alpha = 0;
                tss.third.GetComponent<CanvasGroup>().alpha = 0;*/
                tss.GetComponent<CanvasGroup>().alpha = 1;
                turretshop.SetActive(false);

                Vector3 grid_pos = currpos - g.transform.position;
                GameObject currcube = g.gameObject.transform.GetChild((int)grid_pos.x * g.h + (int)grid_pos.z).gameObject;
                currcube.GetComponent<Renderer>().material = cubenolight;


                GameObject highlighter = g.gameObject.transform.GetChild(g.gameObject.transform.childCount - 1).gameObject;
                Color c = Color.white;
                c.a = 0f;
                highlighter.GetComponent<Renderer>().material.color = c;
            }

            if (state == 0)
            {

                if (gold.balance >= gold.turr_cost)
                {
                    /*Vector3 mousepos = Input.mousePosition;
                    mousepos.z = cam.transform.position.z;
                    Vector3 mp = GetWorldPositionOnPlane(mousepos, 0f);
                    Vector3 currpos = Gridize(new Vector3(mp.x, 0, mp.z));*/
                    // press 1 key to spawn a passive building
                    if (/*Input.GetKeyDown(KeyCode.Alpha1) ||*/ (g1s == 1))
                    {
                        tss.GetComponent<CanvasGroup>().alpha = 1;
                        GameObject hello = GameObject.Instantiate(turr_pf1,currpos, Quaternion.identity, bh_transform);
                        //hello.transform.position = new Vector3(0, 2, 0);
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 1;
                    }
                    if (/*Input.GetKeyDown(KeyCode.Alpha2) ||*/ (g2s == 1))
                    {
                        if (tss.missile){
                            tss.GetComponent<CanvasGroup>().alpha = 1;
                            GameObject hello = GameObject.Instantiate(turr_pf2, currpos, Quaternion.identity,bh_transform);
                            //hello.transform.position = new Vector3(0, 2, 0);
                            hello.AddComponent<AudioSource>();
                            hello.GetComponent<Building>().parent_grid = gameObject;
                            newmove = g.transform.position;
                            state = 2;}
                    }
                    if (/*Input.GetKeyDown(KeyCode.Alpha3) ||*/ (g3s == 1))
                    {
                        if (tss.slow){
                            tss.GetComponent<CanvasGroup>().alpha = 1;
                            GameObject hello = GameObject.Instantiate(turr_pf3, currpos,Quaternion.identity, bh_transform);
                            //hello.transform.position = new Vector3(0, 2, 0);
                            hello.AddComponent<AudioSource>();
                            hello.GetComponent<Building>().parent_grid = gameObject;
                            newmove = g.transform.position;
                            state = 3;}
                    }


                }
                else {
                    ng.Fade();
                }
            }
            else
            {

                GameObject go = GameObject.FindObjectOfType<AudioSource>().gameObject;
                // code for moving with mouse
                /*Vector3 mousepos = Input.mousePosition;
                mousepos.z = cam.transform.position.z;
                Vector3 mp = GetWorldPositionOnPlane(mousepos, 0f);
                Vector3 currpos = Gridize(new Vector3(mp.x, 0, mp.z));    */

                if (Input.GetKeyDown(KeyCode.Escape)){
                    tss.GetComponent<CanvasGroup>().alpha = 0;
                    
                    state = 0;
                    awake = false;
                    blocker.SetActive(false);
                    turretshop.SetActive(false);
                    GameObject highlighter = g.gameObject.transform.GetChild(g.gameObject.transform.childCount - 1).gameObject;
                    Color c = Color.white;
                    c.a = 0f;
                    highlighter.GetComponent<Renderer>().material.color = c;
                    Object.Destroy(go);
                }
                            
                // if we are currently in state 1 and select a different tower
                if (state == 1){

                    if (/*Input.GetKeyDown(KeyCode.Alpha2) ||*/ (g2s == 1) ){
                        if (tss.missile){
                            group1.GetComponent<MouseEnterStore>().state = 0;
                            /*tss.first.GetComponent<CanvasGroup>().alpha = 0;
                            tss.second.GetComponent<CanvasGroup>().alpha = 1;*/
                            GameObject.Destroy(go);
                            GameObject hello = GameObject.Instantiate(turr_pf2,currpos,Quaternion.identity, bh_transform);
                            //Vector3 pos = new Vector3(0, 2, 0);
                            hello.AddComponent<AudioSource>();
                            hello.GetComponent<Building>().parent_grid = gameObject;
                            newmove = g.transform.position;
                            state = 2;}
                    }
                    if (/*Input.GetKeyDown(KeyCode.Alpha3) ||*/ (g3s == 1) ){
                        if (tss.slow){
                            group1.GetComponent<MouseEnterStore>().state = 0;
                            /*tss.first.GetComponent<CanvasGroup>().alpha = 0;
                            tss.third.GetComponent<CanvasGroup>().alpha = 1;*/
                            GameObject.Destroy(go);
                            GameObject hello = GameObject.Instantiate(turr_pf3, currpos,Quaternion.identity, bh_transform);
                            //hello.transform.position = new Vector3(0, 2, 0);
                            hello.AddComponent<AudioSource>();
                            hello.GetComponent<Building>().parent_grid = gameObject;
                            newmove = g.transform.position;
                            state = 3;}
                    }
                }
                // if we are currently in state 2 and select a different tower
                if (state == 2){
                    if (/*Input.GetKeyDown(KeyCode.Alpha1) ||*/ (g1s == 1) ){
                        group2.GetComponent<MouseEnterStore>().state = 0;
                        /*tss.second.GetComponent<CanvasGroup>().alpha = 0;
                        tss.first.GetComponent<CanvasGroup>().alpha = 1;*/
                        GameObject.Destroy(go);
                        GameObject hello = GameObject.Instantiate(turr_pf1, currpos,Quaternion.identity, bh_transform);
                        //hello.transform.position = new Vector3(0, 2, 0);
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 1;
                    }
                    if (/*Input.GetKeyDown(KeyCode.Alpha3) ||*/ (g3s == 1) ){
                        if (tss.slow){
                            group2.GetComponent<MouseEnterStore>().state = 0;
                            /*tss.third.GetComponent<CanvasGroup>().alpha = 1;
                            tss.second.GetComponent<CanvasGroup>().alpha = 0;*/
                            GameObject.Destroy(go);
                            GameObject hello = GameObject.Instantiate(turr_pf3, currpos,Quaternion.identity, bh_transform);
                            //hello.transform.position = new Vector3(0, 2, 0);
                            hello.AddComponent<AudioSource>();
                            hello.GetComponent<Building>().parent_grid = gameObject;
                            newmove = g.transform.position;
                            state = 3;}
                    }
                }
                // if we are currently in state 3 and select a different tower
                if (state == 3){
                    if (/*Input.GetKeyDown(KeyCode.Alpha2) ||*/ (g2s == 1) ){
                        if (tss.missile){
                            group3.GetComponent<MouseEnterStore>().state = 0;
                            /*tss.second.GetComponent<CanvasGroup>().alpha = 1;
                            tss.third.GetComponent<CanvasGroup>().alpha = 0;*/
                            GameObject.Destroy(go);
                            GameObject hello = GameObject.Instantiate(turr_pf2, currpos,Quaternion.identity, bh_transform);
                            //hello.transform.position = new Vector3(0, 2, 0);
                            hello.AddComponent<AudioSource>();
                            hello.GetComponent<Building>().parent_grid = gameObject;
                            newmove = g.transform.position;
                            state = 2;}
                    }
                    if (/*Input.GetKeyDown(KeyCode.Alpha1)||*/ (g1s == 1)){
                        group3.GetComponent<MouseEnterStore>().state = 0;
                        /*tss.third.GetComponent<CanvasGroup>().alpha = 1;
                        tss.first.GetComponent<CanvasGroup>().alpha = 0;*/
                        GameObject.Destroy(go);
                        GameObject hello = GameObject.Instantiate(turr_pf1, currpos,Quaternion.identity, bh_transform);
                        //hello.transform.position = new Vector3(0, 2, 0);
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 1;
                    }
                }

                //go.transform.position = Gridize(new Vector3(mp.x, 0, mp.z));
                go.transform.position = currpos;
                if (Input.GetMouseButtonDown(0) || (group1.GetComponent<MouseEnterStore>().clicked == 1) || (group2.GetComponent<MouseEnterStore>().clicked == 1) || (group3.GetComponent<MouseEnterStore>().clicked == 1))
                {
                    Vector3 grid_pos = go.transform.position - g.transform.position;

                    if (!Occupied(grid_pos))
                    {
                        tss.GetComponent<CanvasGroup>().alpha = 0;
                        /*tss.first.GetComponent<CanvasGroup>().alpha = 0;
                        tss.second.GetComponent<CanvasGroup>().alpha = 0;
                        tss.third.GetComponent<CanvasGroup>().alpha = 0;*/
                        group1.GetComponent<MouseEnterStore>().state = 0;
                        group2.GetComponent<MouseEnterStore>().state = 0;
                        group3.GetComponent<MouseEnterStore>().state = 0;
                
                        GameObject currcube = g.gameObject.transform.GetChild((int)grid_pos.x * g.h + (int)grid_pos.z).gameObject;
                        currcube.GetComponent<Renderer>().material = cubenolight;
                        
                        turretshop.SetActive(false);
                        Occupy(grid_pos);
                        state = 0;
                        awake = false;
                        go.GetComponent<Turret>().awake = true;
                        go.GetComponent<Building>().alive = true;
                        gold.balance -= gold.turr_cost;
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
