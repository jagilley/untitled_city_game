using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Sphere;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating ("SpawnNext", 10f, Random.Range(1f, 2f));
    }

    void Awake(){
        Invoke("SpawnNext", 5f);
    }
    void SpawnNext(){
        
        GameObject new_enemy = Instantiate(Sphere, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject new_enemy = Instantiate(Sphere);
    }
}
