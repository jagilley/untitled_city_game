using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldText : MonoBehaviour
{
    
    private GoldManager gold;
    
    // Start is called before the first frame update
    void Start()
    {
        gold = GameObject.FindObjectOfType<GoldManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = gold.balance.ToString();
    }
}
