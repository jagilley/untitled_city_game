using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseEnterStore : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public int state;
    public int clicked;
    public Image im;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        clicked = 0;
        im.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void OnPointerEnter(PointerEventData eventData){
        state = 1;
        print("hi");
        im.gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void OnPointerExit(PointerEventData eventData){
        state = 0;
        print("hi");
        im.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }


    public void OnPointerClick(PointerEventData eventData){
        im.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        clicked = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0){
            im.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }
    }
}
