﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShop : MonoBehaviour
{
    public GameObject first;
    public GameObject second;
    public GameObject third;
    public GameObject turrgridholder;
    
    // Start is called before the first frame update
    void Start()
    {
        first.GetComponent<CanvasGroup>().alpha = 0;
        second.GetComponent<CanvasGroup>().alpha = 0;
        third.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}