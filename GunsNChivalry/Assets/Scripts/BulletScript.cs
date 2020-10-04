using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private int damages;
    private int pierce;
    private float range;

    public void Setup(Vector2 force, float _dmgs, float _range, float _pierce)
    {
        damages = Mathf.CeilToInt(_dmgs);
        pierce = Mathf.FloorToInt(_pierce);
        range = _range / 10f;

        GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    private void Update()
    {
        range -= Time.deltaTime;

        if (range < 0 || pierce < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Enemy"))
        {
            pierce--;
            c.gameObject.GetComponentInParent<EnemyPathFollow>().TakeDamges(damages);
        }
    }

}
