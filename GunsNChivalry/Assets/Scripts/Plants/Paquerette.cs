using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paquerette : MonoBehaviour
{
    [Header("Stats")]
    public static int price = 100;
    [SerializeField] private int damages = 0;
    [SerializeField] private float fireRate = 0f;
    [SerializeField] private int pierce = 0;
    [SerializeField] private float force = 0f;
    [SerializeField] private float range = 0f;

    [Header("Misc")]
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private GameObject rangeObject = null;
    private bool canShoot = true;
    private GameObject target = null;

    [Header("Evolution")]
    [SerializeField] private Sprite stage1;
    [SerializeField] private Sprite stage2;

    private void Start() => rangeObject.transform.localScale = new Vector3(range * 2f, range * 2f, 1f);

    void Update()
    {
        Targetting();

        if(canShoot && target != null)
            StartCoroutine(Shoot());
    }

    private void Targetting()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range);

        if (enemies.Length <= 0)
            target = null;
        else if (enemies.Length > 0)
        {
            int best = 0, index = 0, i = 0;

            foreach (Collider2D e in enemies)
            {
                EnemyPathFollow epf = e.GetComponent<EnemyPathFollow>();
                if (epf != null)
                {
                    if (epf.distance > best)
                    {
                        best = (int)epf.distance;
                        index = i;
                    }
                }

                i++;
            }

            if (enemies[index].gameObject.CompareTag("Enemy"))
                target = enemies[index].gameObject;
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        Vector2 forceDirection = (target.transform.position - transform.position).normalized;

        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        b.GetComponent<BulletScript>().Setup(forceDirection * force, damages, range, pierce);

        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }
}
