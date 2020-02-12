using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 50f;

    public void Chaser(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distancePF = speed * Time.deltaTime;

        if (dir.magnitude <= distancePF)
        {
            TargetHit();
            return;
        }

        transform.Translate(dir.normalized * distancePF, Space.World);
    }

    void TargetHit()
    {
        //Debug.Log("hit target");
        Destroy(gameObject);
    }
}
