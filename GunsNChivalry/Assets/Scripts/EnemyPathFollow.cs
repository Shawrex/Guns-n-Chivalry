using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class EnemyPathFollow : MonoBehaviour
{
    [SerializeField] public int life;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private int lastPoint = 0;
    public long distance;
    private Vector2 moveVec;
    private Rigidbody2D rb;
    PathAndWaveGeneration room;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        room = RoomsTemplates.instance.roomsSpawned[RoomsTemplates.instance.currentRoom].GetComponent<PathAndWaveGeneration>();
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Point"))
        {
            if (lastPoint == room.roomPath.Count - 1)
                Destroy(gameObject);
            else if (room.roomPath.IndexOf(c.transform) == lastPoint)
                lastPoint++;
        }
    }

    public void TakeDamges(int damages)
    {
        life -= damages;

        if (life <= 0)
        {
            ScoreScript.instance.ChangeScore("money", 15);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (lastPoint >= 0)
        {
            Vector2 moveDir = room.roomPath[lastPoint].position - transform.position;
            moveVec = moveDir.normalized * moveSpeed;
        }

        transform.up = moveVec;
    }

    private void FixedUpdate()
    {
        rb.position += moveVec * Time.fixedDeltaTime;
        distance += (long)Math.Abs(moveVec.magnitude);
    }
}
