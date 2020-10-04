using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class EnemyPathFollow : MonoBehaviour
{
    public int life;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private int lastPoint = 0;
    public long distance;
    private Vector2 moveVec;
    private Rigidbody2D rb;
    PathAndWaveGeneration room;
    private List<Transform> pathToFollow = new List<Transform>();

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        room = RoomsTemplates.instance.roomsSpawned[RoomsTemplates.instance.currentRoom].GetComponent<PathAndWaveGeneration>();
        pathToFollow = room.roomPath;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Point"))
        {
            if (lastPoint == pathToFollow.Count - 1)
            {
                room.enemiesAlive.Remove(gameObject);
                Destroy(gameObject);
            }
            else if (pathToFollow.IndexOf(c.transform) == lastPoint)
                lastPoint++;
        }
    }

    public void TakeDamges(int damages)
    {
        life -= damages;

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (lastPoint >= 0)
        {
            Vector2 moveDir = pathToFollow[lastPoint].position - transform.position;
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
