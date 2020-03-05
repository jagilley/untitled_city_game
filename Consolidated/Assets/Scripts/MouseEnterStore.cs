using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseEnterStore : MonoBehaviour, IPointerClickHandler
{
    public int state;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
    }

    public void OnPointerClick(PointerEventData eventData){
        state = 1;
        print("hi");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
