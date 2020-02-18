using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurrPlacer : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        //initialize literally everything
        //gold manager
        gold = GameObject.FindObjectOfType<GoldManager>();
        g = gameObject.GetComponent<GridManager>();
        //box collider for the grid to know when its clicked on
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        b.center = b.center + new Vector3(0, g.h / 2, g.w / 2);
        b.size = new Vector3(1, 1, g.w + 1);
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
        if (!EventSystem.current.IsPointerOverGameObject() && gold.balance >= gold.turr_cost)
        {
            if (awake == false)
            {
                awake = true;
                turretshop.SetActive(true);
                blocker.SetActive(true);
                GameObject highlighter = g.gameObject.transform.GetChild(g.gameObject.transform.childCount - 1).gameObject;
                Color c = Color.white;
                c.a = .2f;
                highlighter.GetComponent<Renderer>().material.color = c;
                print("woke up");
            }

            else
            {
                awake = false;
                turretshop.SetActive(false);
                print("sleep");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (awake == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape)){
                awake = false;
                blocker.SetActive(false);
                tss.first.GetComponent<CanvasGroup>().alpha = 0;
                tss.second.GetComponent<CanvasGroup>().alpha = 0;
                tss.third.GetComponent<CanvasGroup>().alpha = 0;
                turretshop.SetActive(false);
                GameObject highlighter = g.gameObject.transform.GetChild(g.gameObject.transform.childCount - 1).gameObject;
                Color c = Color.white;
                c.a = 0f;
                highlighter.GetComponent<Renderer>().material.color = c;
            }

            if (state == 0)
            {

                if (gold.balance >= gold.turr_cost)
                {
                    // press 1 key to spawn a passive building
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        tss.first.GetComponent<CanvasGroup>().alpha = 1;
                        GameObject hello = GameObject.Instantiate(turr_pf1, bh_transform);
                        hello.transform.position = new Vector3(0, 2, 0);
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 1;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        tss.second.GetComponent<CanvasGroup>().alpha = 1;
                        GameObject hello = GameObject.Instantiate(turr_pf2, bh_transform);
                        hello.transform.position = new Vector3(0, 2, 0);
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 2;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        tss.third.GetComponent<CanvasGroup>().alpha = 1;
                        GameObject hello = GameObject.Instantiate(turr_pf3, bh_transform);
                        hello.transform.position = new Vector3(0, 2, 0);
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 3;
                    }


                }
            }
            else
            {

                GameObject go = GameObject.FindObjectOfType<AudioSource>().gameObject;
                // code for moving with mouse
                Vector3 mousepos = Input.mousePosition;
                mousepos.z = 12;
                Vector3 mp = cam.GetComponent<Camera>().ScreenToWorldPoint(mousepos);

                if (Input.GetKeyDown(KeyCode.Escape)){
                    tss.first.GetComponent<CanvasGroup>().alpha = 0;
                    tss.second.GetComponent<CanvasGroup>().alpha = 0;
                    tss.third.GetComponent<CanvasGroup>().alpha = 0;
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
                    if (Input.GetKeyDown(KeyCode.Alpha2)){
                        tss.first.GetComponent<CanvasGroup>().alpha = 0;
                        tss.second.GetComponent<CanvasGroup>().alpha = 1;
                        GameObject.Destroy(go);
                        GameObject hello = GameObject.Instantiate(turr_pf2, bh_transform);
                        hello.transform.position = new Vector3(0, 2, 0);
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 2;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3)){
                        tss.first.GetComponent<CanvasGroup>().alpha = 0;
                        tss.third.GetComponent<CanvasGroup>().alpha = 1;
                        GameObject.Destroy(go);
                        GameObject hello = GameObject.Instantiate(turr_pf3, bh_transform);
                        hello.transform.position = new Vector3(0, 2, 0);
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 3;
                    }
                }
                // if we are currently in state 2 and select a different tower
                if (state == 2){
                    if (Input.GetKeyDown(KeyCode.Alpha1)){
                        tss.second.GetComponent<CanvasGroup>().alpha = 0;
                        tss.first.GetComponent<CanvasGroup>().alpha = 1;
                        GameObject.Destroy(go);
                        GameObject hello = GameObject.Instantiate(turr_pf1, bh_transform);
                        hello.transform.position = new Vector3(0, 2, 0);
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 1;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha3)){
                        tss.third.GetComponent<CanvasGroup>().alpha = 1;
                        tss.second.GetComponent<CanvasGroup>().alpha = 0;
                        GameObject.Destroy(go);
                        GameObject hello = GameObject.Instantiate(turr_pf3, bh_transform);
                        hello.transform.position = new Vector3(0, 2, 0);
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 3;
                    }
                }
                // if we are currently in state 3 and select a different tower
                if (state == 3){
                    if (Input.GetKeyDown(KeyCode.Alpha2)){
                        tss.second.GetComponent<CanvasGroup>().alpha = 1;
                        tss.third.GetComponent<CanvasGroup>().alpha = 0;
                        GameObject.Destroy(go);
                        GameObject hello = GameObject.Instantiate(turr_pf2, bh_transform);
                        hello.transform.position = new Vector3(0, 2, 0);
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 2;
                    }
                    if (Input.GetKeyDown(KeyCode.Alpha1)){
                        tss.third.GetComponent<CanvasGroup>().alpha = 1;
                        tss.first.GetComponent<CanvasGroup>().alpha = 0;
                        GameObject.Destroy(go);
                        GameObject hello = GameObject.Instantiate(turr_pf1, bh_transform);
                        hello.transform.position = new Vector3(0, 2, 0);
                        hello.AddComponent<AudioSource>();
                        hello.GetComponent<Building>().parent_grid = gameObject;
                        newmove = g.transform.position;
                        state = 1;
                    }
                }

                go.transform.position = Gridize(new Vector3(mp.x, 0, mp.z));
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 grid_pos = go.transform.position - g.transform.position;

                    if (!Occupied(grid_pos))
                    {
                        tss.first.GetComponent<CanvasGroup>().alpha = 0;
                        tss.second.GetComponent<CanvasGroup>().alpha = 0;
                        tss.third.GetComponent<CanvasGroup>().alpha = 0;
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
