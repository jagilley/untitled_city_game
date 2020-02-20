using System.Collections;
using System.Collections.Generic;
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
    private static Color matColor;
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
        buildSprite.color = matColor;
    }

    public static void SetName(string newName)
    {
        buildName = newName;
    }
    public static void SetDamage(float newDmg)
    {
        PS = "DPS: " + newDmg;
    }

    public static void SetGold(float gold)
    {
        PS = "GG: " + gold;
    }

    public static void SetPrice(float price)
    {
        SC = "Sell For: " + price + "G";
    }

    public static void SetSprite(Color color)
    {
        matColor = color;
    }
}
