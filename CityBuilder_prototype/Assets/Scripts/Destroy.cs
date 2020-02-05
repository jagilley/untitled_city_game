using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroy : MonoBehaviour
{

    private Button btn;
    private Building_Holder bh;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(DestroyClick);
        bh = GameObject.FindObjectOfType<Building_Holder>();
    }

    void DestroyClick(){
        bh.Destroy_Active();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
