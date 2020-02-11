using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder: MonoBehaviour
{
    public GameObject prefab;
    public Camera notmainCamera;

    public Vector3 positionOffset = new Vector3(0f, 0.25f, 0f);
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos;
            Ray ray = notmainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                worldPos = hit.point;
                Instantiate(prefab, worldPos + positionOffset, Quaternion.identity);
            }
            else
            {
                Debug.Log("Can't build there!'");
            }
        }
    }


}
