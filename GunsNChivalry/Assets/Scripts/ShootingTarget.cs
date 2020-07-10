using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTarget : MonoBehaviour
{
    [SerializeField] private float shootDelay = 0;
    [SerializeField] private float shootForce = 0;
    [SerializeField] private GameObject bulletPrefab = null;

    private bool canShoot = true;

    void Update()
    {
        if (canShoot && GetComponent<RotationTargeting>().target != null)
            StartCoroutine(Shoot());
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
