using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseEnterStore : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public int state;
    public int clicked;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        clicked = 0;
    }

    public void OnPointerEnter(PointerEventData eventData){
        state = 1;
        print("hi");
    }

    public void OnPointerClick(PointerEventData eventData){
        clicked = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
