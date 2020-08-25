using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    [Header("Stats")]
    public static int price = 175;
    [SerializeField] private int damages = 0;
    [SerializeField] private float fireRate = 0f;
    [SerializeField] private float numberOfShots = 0f;
    [SerializeField] private int pierce = 0;
    [SerializeField] private float force = 0f;
    [SerializeField] private float range = 0f;

    [Header("Misc")]
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private GameObject rangeObject = null;
    private bool canShoot = true;

    [Header("Evolution")]
    [SerializeField] private Sprite stage1;
    [SerializeField] private Sprite stage2;

    private void Start() => rangeObject.transform.localScale = new Vector3(range * 2f, range * 2f, 1f);

    void Update()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range);

        foreach (Collider2D e in enemies)
        {
            if (e.GetComponent<EnemyPathFollow>() != null && canShoot)
                StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        for (int i = 0; i < numberOfShots; i++)
        {
            float angle = 360 / numberOfShots * i;
            Vector2 forceDirection = (Quaternion.Euler(0, 0, angle) * Vector2.right);

            GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            b.GetComponent<BulletScript>().Setup(forceDirection * force, damages, range, pierce);
        }

        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }
}
