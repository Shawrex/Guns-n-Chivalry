using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManaging : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesPrefabs = null;
    [SerializeField] private float spawnTime = 0f;
    public List<GameObject> enemies;

    [SerializeField] private Transform[] pathPoints = null;

    public static EnemyManaging instance;

    private void Start()
    {
        if (instance == null)
            instance = this;

        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnTime);
        Spawn();
    }

    private void Spawn()
    {
        GameObject e = Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)], pathPoints[0].position, Quaternion.identity);
        enemies.Add(e);

        StartCoroutine(SpawnTimer());
    }
}
