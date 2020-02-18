using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public int w;
    public int h;
    private int[,] gridArray;
    private float size = .5f;
    public int[] taken;
    private Material transparent;

    void Start()
    {
        transparent = Resources.Load("GridCube", typeof(Material)) as Material;
        gridArray = new int[w, h];
        this.taken = new int[w * h];
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                temp.transform.parent = gameObject.transform;
                temp.transform.position = new Vector3(i, 0, j) + gameObject.transform.position;
                temp.transform.localScale = new Vector3(size, size, size);
                taken[i * w + j] = 0;
            }
        }
        GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
        box.transform.parent = gameObject.transform;
        box.transform.position = gameObject.transform.position + new Vector3(w / 2f - .5f, 0f, h / 2f - .5f);
        print(box.transform.position);
        box.transform.localScale = new Vector3(w, 0.5f, h);
        Color c = Color.white;
        c.a = 0f;
        box.GetComponent<Renderer>().material = transparent;
        box.GetComponent<Renderer>().material.color = c;
        //box.SetActive(false); 
    }


}