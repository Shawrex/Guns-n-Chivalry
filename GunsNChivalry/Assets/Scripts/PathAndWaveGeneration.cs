using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAndWaveGeneration : MonoBehaviour
{
    [Header("Path")]
    private int pathPointsNum;
    public List<Transform> roomPath = null;
    public List<GameObject> roomPathRender = null;
    private Vector2 pos;
    [SerializeField] private GameObject pathPoint = null;
    [SerializeField] private GameObject pathRender = null;

    [Header("Wave")]
    [SerializeField] private GameObject[] enemiesPrefabs = null;
    [SerializeField] private float spawnTime = 0f;
    [SerializeField] private int waveSpawnCount = 0;
    private bool spawned = false;

    public List<GameObject> enemiesAlive = new List<GameObject>();

    void Awake()
    {
        Physics2D.IgnoreLayerCollision(0, 8);
        Physics2D.IgnoreLayerCollision(8, 8);
        Physics2D.IgnoreLayerCollision(8, 9);

        if (gameObject.name != "Base")
        {
            SetupRoom();

            GeneratePath();

            Exited();
        }
    }

    private void SetupRoom()
    {
        int difficulty = Random.Range(0, 3);

        switch (difficulty)
        {
            case 0: //EASY
                pathPointsNum = Random.Range(8, 10);
                waveSpawnCount = Random.Range(15, 20);
                spawnTime = Random.Range(0.5f, 0.75f);
                break;

            case 1: //MEDIUM
                pathPointsNum = Random.Range(6, 8);
                waveSpawnCount = Random.Range(15, 25);
                spawnTime = Random.Range(0.25f, 0.75f);
                break;

            case 2: //HARD
                pathPointsNum = Random.Range(5, 6);
                waveSpawnCount = Random.Range(20, 30);
                spawnTime = Random.Range(0.25f, 0.5f);
                break;
        }
    }

    void GeneratePath()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        for (int i = 0; i < pathPointsNum; i++)
        {
            if (i == 0 || i == pathPointsNum - 1)
            {
                pos = children[UnityEngine.Random.Range(1, children.Length)].localPosition.normalized;
                pos = new Vector2(pos.x * 9.1f, pos.y * 5.1f);
            }
            else
                pos = new Vector2(UnityEngine.Random.Range(-7f, 7f), UnityEngine.Random.Range(-4f, 4f));


            GameObject p = Instantiate(pathPoint, transform);
            p.transform.localPosition = pos;
            roomPath.Add(p.transform);
        }

        for (int i = 0; i < pathPointsNum - 1; i++)
        {
            Vector2 pos1 = roomPath[i].localPosition;
            Vector2 pos2 = roomPath[i + 1].localPosition;

            GameObject r = Instantiate(pathRender, transform);
            r.transform.localPosition = (pos1 + pos2) / 2f;
            r.transform.right = pos1 - pos2;
            r.transform.localScale = new Vector3(Vector2.Distance(pos1, pos2), 0.1f, 1f);
            roomPathRender.Add(r);
        }
    }

    public void Exited()
    {
        foreach (GameObject r in roomPathRender)
            r.SetActive(false);
    }

    public void Entered()
    {
        foreach (GameObject r in roomPathRender)
            r.SetActive(true);
    }

    public void PlayWave() => StartCoroutine(Wave());

    private IEnumerator Wave()
    {
        if (!spawned)
        {
            for (int i = 0; i < waveSpawnCount; i++)
            {
                yield return new WaitForSeconds(spawnTime);
                Vector3 spawn = new Vector3(roomPath[0].position.x, roomPath[0].position.y, roomPath[0].position.z - 1f);
                enemiesAlive.Add(Instantiate(enemiesPrefabs[0], spawn, Quaternion.identity));
            }

            spawned = true;
        }

        foreach (GameObject r in RoomsTemplates.instance.roomsSpawned)
            r.GetComponents<PolygonCollider2D>()[1].isTrigger = true;
    }
}
