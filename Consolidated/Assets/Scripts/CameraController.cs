using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private float moveSpeed = .25f;
    public int exploration;

    // Start is called before the first frame update
    void Start()
    {
        exploration = -1;
    }

    public void PanOver(){
        StartCoroutine(PanScreen());
    }

    private IEnumerator PanScreen () {
        Vector3 startpos = transform.position;
        Vector3 endpos = new Vector3(23f, 40f, 21f);
        float i = 0f;
        while (i < 1f) {
            i += Time.deltaTime;
            transform.position = Vector3.Lerp(startpos,endpos,i);
            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 40f){
            Vector3 temp = transform.position;
            temp.y = 40f;
            transform.position = temp;
        }

        if (transform.position.z > 40f){
            Vector3 temp = transform.position;
            temp.z = 40f;
            transform.position = temp;
        }

        if (transform.position.x > 40f){
            Vector3 temp = transform.position;
            temp.x = 40f;
            transform.position = temp;
        }

        if (transform.position.x < -40f){
            Vector3 temp = transform.position;
            temp.x = -40f;
            transform.position = temp;
        }
        
        if (transform.position.z < -40f){
            Vector3 temp = transform.position;
            temp.z = -40f;
            transform.position = temp;
        }

        if (transform.position.y < 12f){
            Vector3 temp = transform.position;
            temp.y = 12f;
            transform.position = temp;
        }
        if (exploration == 0){
            if (transform.position.y > 12f){
                Vector3 temp = transform.position;
                temp.y = 12f;
                transform.position = temp;
            }
            if (transform.position.z > 7f){
                Vector3 temp = transform.position;
                temp.z = 7f;
                transform.position = temp;
            }
            if (transform.position.z < -7f){
                Vector3 temp = transform.position;
                temp.z = -7f;
                transform.position = temp;
            }
            if (transform.position.x > 15f){
                Vector3 temp = transform.position;
                temp.x = 15f;
                transform.position = temp;
            }
            if (transform.position.x < -5f){
                Vector3 temp = transform.position;
                temp.x = -5f;
                transform.position = temp;
            }
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.position = transform.position + moveSpeed * new Vector3(1,0,0);
        }
        if (Input.GetKey(KeyCode.A)) {
            transform.position = transform.position - moveSpeed * new Vector3(1,0,0);
        }
        if (Input.GetKey(KeyCode.Q) ) {
            transform.position = transform.position + moveSpeed * new Vector3(0,1,0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            transform.position = transform.position + moveSpeed * new Vector3(0,5,0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            transform.position = transform.position - moveSpeed * new Vector3(0,5,0);
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
