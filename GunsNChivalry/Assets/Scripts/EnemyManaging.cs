using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManaging : MonoBehaviour
{
    public GameObject[] enemies;
    public static EnemyManaging instance;

    void Start()
    {
        if (instance == null)
            instance = this;
    }
}
