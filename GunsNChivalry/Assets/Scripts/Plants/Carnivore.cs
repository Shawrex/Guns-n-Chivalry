using System.Collections;
using UnityEngine;

public class Carnivore : MonoBehaviour
{
    [Header("Stats")]
    public static int price = 75;
    [SerializeField] private float fireRate = 0f;
    [SerializeField] private int capacity = 0;
    [SerializeField] private float range = 0f;

    [Header("Misc")]
    [SerializeField] private GameObject rangeObject = null;
    private GameObject target = null;
    private Animator animator = null;

    [Header("Evolution")]
    [SerializeField] private Sprite stage1;
    [SerializeField] private Sprite stage2;

    void Start()
    {
        rangeObject.transform.localScale = new Vector3(range * 2f, range * 2f, 1f);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Targeting();

        if (capacity > 0 && target != null)
            StartCoroutine(Shoot());
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
                    target = e.gameObject;
                    break;
                }
            }
        }
    }

    IEnumerator Shoot()
    {
        target.GetComponentInParent<EnemyPathFollow>().TakeDamges(3);
        capacity--;
        animator.SetTrigger("Shoot");

        yield return new WaitForSeconds(fireRate);

        target = null;
        capacity++;
    }
}
