using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    public float speed = 50f;
    public float exRadius = 2f;
    public float damage = 15f;


    public void Chaser(Transform _target)
    {
        target = _target;
        //var original = targetEnemy.GetComponent<Renderer>().material.color;
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
        transform.LookAt(target);
    }

    void TargetHit()
    {
        //Debug.Log("hit target");
        

        if (exRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        targetEnemy = enemy.GetComponent<Enemy>();
        targetEnemy.TakeDamage(damage);
    }

    void Explode()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, exRadius);
        foreach (Collider collider in hitObjects)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
                collider.GetComponent<Renderer>().material.color = Color.red;
            }

        }
        
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, exRadius);
    }
}
