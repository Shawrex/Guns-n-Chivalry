using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class EnemyPathFollow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private int lastPoint = 1;
    private Vector2 moveVec;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Point"))
        {
            if (lastPoint == EnemyManaging.instance.pathPoints.Length - 1)
                Die(); //Life--;
            else
                lastPoint++;
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Bullet"))
        {
            Die(); //Score++;
            Destroy(c.gameObject);
        }
    }

    void Die()
    {
        //Life decreses
        EnemyManaging.instance.enemies.Remove(gameObject);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (lastPoint >= 0)
        {
            Vector2 moveDir = EnemyManaging.instance.pathPoints[lastPoint].position - transform.position;
            moveVec = moveDir.normalized * moveSpeed;
        }

        transform.up = moveVec;
    }

    private void FixedUpdate()
    {
        rb.position += moveVec * Time.fixedDeltaTime;
    }
}
