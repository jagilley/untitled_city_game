using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpbar : MonoBehaviour
{
 
    public Enemy senemy;
    public float hp;
    private GameObject green;
    // Start is called before the first frame update
    void Start()
    {
        senemy = gameObject.transform.parent.gameObject.GetComponent<Enemy>();
        hp = senemy.health;
        green = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        hp = senemy.health;
        RectTransform size = green.GetComponent<RectTransform>();
        size.sizeDelta = new Vector2( hp, size.sizeDelta.y);
    }
}
