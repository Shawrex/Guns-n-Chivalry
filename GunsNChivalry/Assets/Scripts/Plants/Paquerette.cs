using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paquerette : MonoBehaviour, IPlantsInterface
{
    [Header("Stats")]
    [SerializeField] private float damages = 0;
    [SerializeField] private float fireRate = 0f;
    [SerializeField] private float pierce = 0;
    [SerializeField] private float force = 0f;
    [SerializeField] private float range = 0f;

    [Header("Misc")]
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private GameObject rangeObject = null;
    [SerializeField] private SpriteRenderer sr = null;
    private bool canShoot = true;

    [Header("Evolution")]
    public int stage;
    private bool isGrowing = false;
    [SerializeField] private float growTime = 0f;
    [SerializeField] private Image growGraph = null;
    public Sprite stage1;
    public Sprite stage2;



    private void Start()
    {
        rangeObject.transform.localScale = new Vector3(range * 2f, range * 2f, 1f);
    }

    void Update()
    {
        if(canShoot)
            Targeting();

        if (isGrowing)
        {
            growGraph.fillAmount += 1 / (growTime * (stage + 1)) * Time.deltaTime;
            growGraph.color = new Color(1f, 1f, 1f, 1 / (growTime * (stage + 1)) * Time.deltaTime + growGraph.color.a);
        }

    }

    void Targeting()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range);

        if (enemies.Length > 0)
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
                StartCoroutine(Shoot(enemies[index].gameObject));
        }
    }

    IEnumerator Shoot(GameObject target)
    {
        canShoot = false;

        Vector2 forceDirection = (target.transform.position - transform.position).normalized;

        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        b.GetComponent<BulletScript>().Setup(forceDirection * force, damages, range, pierce);

        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }

    void IPlantsInterface.Select(bool isSelected) { rangeObject.SetActive(isSelected); }

    IEnumerator IPlantsInterface.Grow()
    {
        if (stage < 2)
        {
            isGrowing = true;

            if (stage == 1)
                growGraph.sprite = stage2;

            yield return new WaitForSeconds(growTime * (stage + 1));

            LevelUp();
        }
    }

    public void LevelUp()
    {
        damages += 0.5f;
        fireRate -= 0.075f;
        pierce += 0.5f;
        force += 2;
        range += 0.5f;

        stage++;

        if (stage == 1)
            sr.sprite = stage1;
        else
            sr.sprite = stage2;

        isGrowing = false;
        growGraph.fillAmount = 0f;
    }

    int IPlantsInterface.GetType() { return 0; }

    int IPlantsInterface.GetStage() { return stage; }
}
