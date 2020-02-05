using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    private GameObject menu_holder;
    public bool awake;
    public int selfnum;
    private Building_Holder bh;
    
    // Start is called before the first frame update
    void Start()
    {
        menu_holder = Object.FindObjectOfType<CanvasGroup>().gameObject;
        awake = false;
        bh = GameObject.FindObjectOfType<Building_Holder>();
        selfnum = bh.num_build;
    }

    void OnMouseDown(){
        
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
