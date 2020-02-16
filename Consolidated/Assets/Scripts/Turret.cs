using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    private Enemy targetEnemy;
    public float range = 15f;

    public Transform rotater;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public LineRenderer lineRenderer;
    public bool usingLaser = false;
    public int damageOT = 20;

    // george - I need the turrets to not fire until you place them
    public bool awake;



    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        awake = false;
    }
    
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && minDistance <= range)
        {
            target = closestEnemy.transform;
            targetEnemy = closestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (awake == false){
            if (usingLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                }
            }
            return;
        }
        else {          
            if (target == null)
            {
                if (usingLaser)
                {
                    if (lineRenderer.enabled)
                    {
                        lineRenderer.enabled = false;
                    }
                }
                return;
            }


            LockOn();

            if (usingLaser)
            {
                Lasering();
            }
            else
            {
                if (fireCountdown <= 0)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }

                fireCountdown -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Chaser(target);
        }
    }

    void LockOn()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion LRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = LRotation.eulerAngles;
        rotater.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Lasering()
    {
        targetEnemy.TakeDamage(damageOT * Time.deltaTime);
        targetEnemy.speed = 1.5f;
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
