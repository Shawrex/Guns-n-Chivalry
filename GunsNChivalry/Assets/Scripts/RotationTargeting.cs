using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTargeting : MonoBehaviour
{
    private string targeting;
    private GameObject target;

    private void Start()
    {
        targeting = "first";
    }

    void LateUpdate()
    {
        List<GameObject> enemies = EnemyManaging.instance.enemies;

        if (targeting == "first" && enemies.Count > 0)
        {
            target = enemies[0];
        }
        
        if (target != null)
            transform.up = target.transform.position;
    }
}
