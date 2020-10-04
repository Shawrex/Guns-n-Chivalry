using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cactus : MonoBehaviour, IPlantsInterface
{
    [Header("Stats")]
    [SerializeField] private float damages = 0;
    [SerializeField] private float fireRate = 0f;
    [SerializeField] private int numberOfShots = 0;
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
    private bool isGrowing;
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
        if (canShoot)
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
            foreach (Collider2D e in enemies)
            {
                if (e.CompareTag("Enemy"))
                {
                    StartCoroutine(Shoot());
                    break;
                }
            }
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
        damages += 0.25f;
        fireRate -= 0.1f;
        force += 2f;
        range += 0.5f;
        numberOfShots += 2;

        stage++;

        if (stage == 1)
            sr.sprite = stage1;
        else
            sr.sprite = stage2;

        isGrowing = false;
        growGraph.fillAmount = 0f;
    }

    int IPlantsInterface.GetType() { return 1; }

    int IPlantsInterface.GetStage() { return stage; }
}
