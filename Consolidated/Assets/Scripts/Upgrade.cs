using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    private Button btn;
    private Building_Holder bh;
    public int Upgrade_Cost;

    // Start is called before the first frame update
    void Start()
    {
        bh = GameObject.FindObjectOfType<Building_Holder>();
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(UpgradeClick);
        Upgrade_Cost = 25;
    }

    void UpgradeClick()
    {
        bh.Upgrade_Active();
        bh.GetComponent<GoldManager>().spendUpgrade(Upgrade_Cost);
        Upgrade_Cost += 25;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
