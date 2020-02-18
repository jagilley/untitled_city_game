using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseEnterStore : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject attached;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData){
        attached.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void OnPointerExit(PointerEventData eventData){
        attached.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
