﻿using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class EnemyPathFollow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private int lastPoint = 0;
    public long distance;
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
            if (lastPoint == PathAndWaveGeneration.instance.roomPath.Count - 1)
                Die(); //Life--;
            else if (PathAndWaveGeneration.instance.roomPath.IndexOf(c.transform) >= lastPoint)
                lastPoint++;
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Bullet"))
        {
            Die();
            ScoreScript.instance.ChangeScore(1);
        }
    }

    void Die()
    {
        //Life decreases
        Destroy(gameObject);
    }

    private void Update()
    {
        if (lastPoint >= 0)
        {
            Vector2 moveDir = PathAndWaveGeneration.instance.roomPath[lastPoint].position - transform.position;
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
