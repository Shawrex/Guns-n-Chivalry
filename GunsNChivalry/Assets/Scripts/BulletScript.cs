using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int pierce;
    public float range;

    private void Update()
    {
        range -= Time.deltaTime;

        if (range < 0 || pierce < 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Enemy"))
            pierce--;
    }

}
