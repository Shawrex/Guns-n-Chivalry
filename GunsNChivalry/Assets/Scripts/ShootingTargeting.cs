using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTargeting : MonoBehaviour
{
    [SerializeField] private float radius = 0;
    [SerializeField] private string targeting = null;
    [SerializeField] private float shootDelay = 0;
    [SerializeField] private float shootForce = 0;
    [SerializeField] private GameObject bulletPrefab = null;

    public GameObject target;

    private bool canShoot = true;

    void Update()
    {
        if (canShoot && target != null)
            StartCoroutine(Shoot());
    }

    void LateUpdate()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius);

        if (enemies.Length <= 0)
            target = null;

        if (targeting == "first" && enemies.Length > 0)
        {
            long highest = 0;
            int index = 0, i = 0;

            foreach (Collider2D e in enemies)
            {
                EnemyPathFollow epf = e.GetComponent<EnemyPathFollow>();
                if (epf != null)
                {
                    if (epf.distance > highest)
                    {
                        highest = epf.distance;
                        index = i;
                    }
                }

                i++;
            }

            if (enemies[index].gameObject.CompareTag("Enemy"))
                target = enemies[index].gameObject;
        }

        if (target != null)
            transform.up = target.transform.position - transform.position;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody2D>().AddForce(transform.up * shootForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
