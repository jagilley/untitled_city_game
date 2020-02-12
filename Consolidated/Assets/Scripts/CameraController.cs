using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private float moveSpeed = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) {
            transform.position = transform.position + moveSpeed * new Vector3(1,0,0);
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.position = transform.position - moveSpeed * new Vector3(1,0,0);
        }
        if (Input.GetKey(KeyCode.Q)) {
            transform.position = transform.position + moveSpeed * new Vector3(0,1,0);
        }
        if (Input.GetKey(KeyCode.E)) {
            transform.position = transform.position - moveSpeed * new Vector3(0,1,0);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.position = transform.position - moveSpeed * new Vector3(0,0,1);
        }
        if (Input.GetKey(KeyCode.W)) {
            transform.position = transform.position + moveSpeed * new Vector3(0,0,1);
        }
    }
}
