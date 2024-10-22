﻿using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UI;

public class StatSelector : MonoBehaviour
{
    // Start is called before the first frame update


    public TMP_Text[] textArr;
    private static string buildName;
    private static string PS;
    private static string SC;
    public Image buildSprite;
    private static Sprite TurrSprite;
    public Image[] images;

    void Start()
    {
        images = GetComponentsInChildren<Image>();
        buildSprite = images[1];
        textArr = GetComponentsInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textArr[0].text = buildName;
        textArr[1].text = PS;
        textArr[2].text = SC;
        buildSprite.sprite = TurrSprite;
    }

    public static void SetName(string newName)
    {
        buildName = newName;
    }

    public static void SetNumber(float numBuild)
    {
        PS = "DmgUp: " + numBuild;
    }

    public static void SetDamage(float newDmg)
    {
        PS = "DMG: " + newDmg;
    }

    public static void SetGold(float gold)
    {
        PS = "Gold/s: " + gold;
    }

    public static void SetPrice(float price)
    {
        SC = "Sell: " + price + " Gold";
    }

    public static void ExploreInfo()
    {
        PS = "Expands Map";
        SC = "0 Gold on Sell";
    }

    public static void SetSprite(Sprite sprite)
    {
        TurrSprite = sprite;
    }
}
