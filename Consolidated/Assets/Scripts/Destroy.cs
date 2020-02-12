using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroy : MonoBehaviour
{

    private Button btn;
    private Building_Holder bh;
    private GameObject menu_holder;

    // Start is called before the first frame update
    void Start()
    {
        menu_holder = Object.FindObjectOfType<CanvasGroup>().gameObject;
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(DestroyClick);
        bh = GameObject.FindObjectOfType<Building_Holder>();
    }

    void DestroyClick(){
        bh.Destroy_Active();
        menu_holder.GetComponent<CanvasGroup>().alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
