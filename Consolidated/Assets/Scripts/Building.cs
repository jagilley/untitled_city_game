using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    private GameObject menu_holder;
    public bool awake;
    public bool alive;
    public int selfnum;
    private Building_Holder bh;
    public GameObject parent_grid;
    
    // Start is called before the first frame update
    void Start()
    {
        menu_holder = Object.FindObjectOfType<CanvasGroup>().gameObject;
        awake = false;
        //should not be live before we drop it into place
        alive = false;
        bh = GameObject.FindObjectOfType<Building_Holder>();
        selfnum = bh.num_build;
    }

    public void UnOccupy(){
        Vector3 gridpos = gameObject.transform.position - parent_grid.transform.position;
        if (parent_grid.GetComponent<test>()){
            parent_grid.GetComponent<test>().UnOccupy(gridpos);}
        else {
            parent_grid.GetComponent<TurrPlacer>().UnOccupy(gridpos);
        }
    }

    void OnMouseDown(){
        if (alive == false){
            return;
        }
        else{
            if (awake == false){
                menu_holder.GetComponent<CanvasGroup>().alpha = 1f;
                awake = true;
                print("hello");
            }
            else {
                menu_holder.GetComponent<CanvasGroup>().alpha = 0f;
                awake = false;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (awake == true){
            gameObject.transform.localScale = new Vector3(2f,2f,2f);
        }
        else {
            gameObject.transform.localScale = new Vector3(1f,1f,1f);
        }
    }
}
