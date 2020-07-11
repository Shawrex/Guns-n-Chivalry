using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManaging : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesPrefabs = null;

    [SerializeField] private float spawnTime = 0f;
    [SerializeField] private int waveSpawnCount = 3;
    private int spawnCount = 0;

    public List<GameObject> enemies;

    public Transform[] pathPoints = null;

    public static EnemyManaging instance;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 8);
        Physics2D.IgnoreLayerCollision(8, 9);

        if (instance == null)
            instance = this;

        spawnCount = waveSpawnCount;

        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnTime);
        Spawn();
    }

    private void Spawn()
    {
        spawnCount--;

        GameObject e = Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)], pathPoints[0].position, Quaternion.identity);
        enemies.Add(e);

        if (spawnCount > 0)
            StartCoroutine(SpawnTimer());
    }
}
