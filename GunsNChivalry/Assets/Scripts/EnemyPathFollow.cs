using System.Collections;
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
                Destroy(gameObject);
            else
                lastPoint++;
        }
    }

    private void Update()
    {
        Vector2 moveDir = EnemyManaging.instance.pathPoints[lastPoint].position - transform.position;
        moveVec = moveDir.normalized * moveSpeed;

        transform.up = moveVec;
    }

    private void FixedUpdate()
    {
        rb.position += moveVec * Time.fixedDeltaTime;
    }
}
