using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildreminder : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public GameObject Blocker;
    
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Blocker.activeSelf == true){
            canvasGroup.alpha = 1;
        }
        else {
            canvasGroup.alpha = 0;
        }
    }
}
