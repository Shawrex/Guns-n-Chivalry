using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    private int placement = 0; //ex : 0 = U -> Need D Door
    private RoomsTemplates rt;
    private bool spawned;

    private void Start()
    {
        Vector3 pos = transform.localPosition;

        if (pos.y > 0)
            placement = 0; //U
        else if (pos.y < 0)
            placement = 1; //D
        else if (pos.x < 0)
            placement = 2; //L
        else if (pos.x > 0)
            placement = 3; //R

        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (!spawned)
        {
            rt = RoomsTemplates.instance;

            if (placement == 0)
                Instantiate(rt.dRooms[Random.Range(0, rt.dRooms.Length)], transform.position, Quaternion.identity); //Spawn a D
            else if (placement == 1)
                Instantiate(rt.uRooms[Random.Range(0, rt.uRooms.Length)], transform.position, Quaternion.identity); //Spawn a U
            else if (placement == 2)
                Instantiate(rt.rRooms[Random.Range(0, rt.rRooms.Length)], transform.position, Quaternion.identity); //Spawn a R
            else if (placement == 3)
                Instantiate(rt.lRooms[Random.Range(0, rt.lRooms.Length)], transform.position, Quaternion.identity); //Spawn a L

            spawned = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("SpawnPoints"))
            Destroy(gameObject);
    }
}
