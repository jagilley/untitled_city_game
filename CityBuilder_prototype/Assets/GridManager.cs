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
    void Start(){
        gridArray = new int[w,h];
        this.taken = new int[w*h];
        for (int i = 0; i < w; i++){
            for (int j = 0; j < h; j++){
                GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                temp.transform.parent = gameObject.transform;
                temp.transform.position = new Vector3(i,j,0) + gameObject.transform.position;
                temp.transform.localScale = new Vector3(size,size,1);
                taken[i*w + j] = 0;
            }
        }
    }   
 
    
}
