using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveShop : MonoBehaviour
{
    public GameObject first;
    public GameObject buildgrid;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        first.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (buildgrid.GetComponent<test>().awake) {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
            //if (Input.GetKeyDown(KeyCode.Alpha1)){
            first.GetComponent<CanvasGroup>().alpha = 1;
            //}
        }
        else {
            first.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.SetActive(false);
        }
        
    }
}
