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
        GameObject[] enemies = EnemyManaging.instance.enemies;

        if (targeting == "first")
        {
            target = enemies[0];
        }

        transform.up = target.transform.position;
    }
}
