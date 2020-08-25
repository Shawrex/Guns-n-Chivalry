using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rose : MonoBehaviour
{
    [Header("Stats")]
    public static int price = 200;
    [SerializeField] private int damages = 0;
    [SerializeField] private float fireRate = 0f;
    [SerializeField] private int pierce;
    [SerializeField] private float range = 0f;

    [Header("Misc")]
    [SerializeField] private GameObject rangeObject = null;
    private bool canShoot = true;
    private GameObject target = null;

    [Header("Evolution")]
    [SerializeField] private Sprite stage1;
    [SerializeField] private Sprite stage2;

    private void Start() => rangeObject.transform.localScale = new Vector3(range * 2f, range * 2f, 1f);

    void Update()
    {
        Targeting();

        if (canShoot && target != null)
            StartCoroutine(Shoot());
    }

    void Targeting()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range);

        if (enemies.Length <= 0)
            target = null;
        else if (enemies.Length > 0)
        {
            int bestL = 0, bestD = 0, index = 0, i = 0;

            foreach (Collider2D e in enemies)
            {
                EnemyPathFollow epf = e.GetComponent<EnemyPathFollow>();
                if (epf != null)
                {
                    if (epf.life > bestL)
                    {
                        bestL = epf.life;
                        bestD = (int)epf.distance;
                        index = i;
                    }
                    else if (epf.life == bestL && epf.distance > bestD)
                    {
                        bestL = epf.life;
                        bestD = (int)epf.distance;
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

        target.GetComponent<EnemyPathFollow>().TakeDamges(damages);

        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }
}
