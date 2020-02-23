using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    private GameObject menu_holder;
    public bool awake;
    public bool alive;
    public int selfnum;
    private Building_Holder bh;
    public GameObject parent_grid;
    public Material normal;
    private Material highlighted;
    private Renderer objectRenderer;

    // Start is called before the first frame update
    void Start()
    {
        menu_holder = Object.FindObjectOfType<ud_holder>().gameObject;
        awake = false;
        //should not be live before we drop it into place
        alive = false;
        bh = GameObject.FindObjectOfType<Building_Holder>();
        selfnum = bh.num_build;
        highlighted = gameObject.GetComponent<MeshRenderer>().material;
        objectRenderer = gameObject.GetComponent<Renderer>();
    }

    public void UnOccupy()
    {
        Vector3 gridpos = gameObject.transform.position - parent_grid.transform.position;
        if (parent_grid.GetComponent<test>())
        {
            parent_grid.GetComponent<test>().UnOccupy(gridpos);
        }
        else
        {
            parent_grid.GetComponent<TurrPlacer>().UnOccupy(gridpos);
        }
    }

    void OnMouseDown()
    {
        if (alive == false)
        {
            return;
        }
        else
        {
            if (awake == false)
            {
                menu_holder.GetComponent<CanvasGroup>().alpha = 1f;
                awake = true;
                StatSelector.SetName(gameObject.tag);
                if (gameObject.tag == "GoldMine")
                {
                    StatSelector.SetGold(10);
                    StatSelector.SetSprite(Color.white);
                }
                else
                {
                    StatSelector.SetDamage(returnDPS());
                }
                StatSelector.SetPrice(50f);

                print("hello");
            }
            else
            {
                menu_holder.GetComponent<CanvasGroup>().alpha = 0f;
                awake = false;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!alive){
            
        }
        else {
            if (awake == true)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    menu_holder.GetComponent<CanvasGroup>().alpha = 0f;
                    awake = false;
                }
                objectRenderer.material = highlighted;
            }
            else
            {
                objectRenderer.material = normal;
            }
        }
    }

    public float returnDPS()
    {
        StatSelector.SetSprite(gameObject.GetComponent<Renderer>().material.color);
        if (gameObject.tag == "LightGun")
        {
            return gameObject.GetComponent<Turret>().returnDamage();
        }
        else if (gameObject.tag == "Laser")
        {
            return gameObject.GetComponent<Turret>().damageOT;
        }
        else if (gameObject.tag == "Missile")
        {
            return gameObject.GetComponent<Turret>().returnDamage();
        }
        return 0;
    }

}