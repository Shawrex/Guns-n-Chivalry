using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    private int placement = 0; //ex : 0 = U -> Need D Door
    private RoomsTemplates rt; //All the rooms templates
    private static int rooms; //The number of rooms spawned ( to keep an -okay- dungeon )
    private GameObject r; //The room that i'll spawn;

    private void Start()
    {
        Vector3 pos = transform.localPosition;

        if (pos.y > 0)
            placement = 0; //I'm a Up point
        else if (pos.y < 0)
            placement = 1; //Down point
        else if (pos.x < 0)
            placement = 2; //Left
        else if (pos.x > 0)
            placement = 3; //Right 

        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        rt = RoomsTemplates.instance; //Reference to the only one rooms templates script ( a sort of database )

        if (placement == 0)
        {
            //Need a room with a Down door

            if (rooms < 20) //If there isn't that much rooms , i can sawn randomly
                r = Instantiate(rt.dRooms[Random.Range(0, rt.dRooms.Length)], transform.position, Quaternion.identity); //randomly
            else
                r = Instantiate(rt.dRoom, transform.position, Quaternion.identity); //No more exits

        }
        else if (placement == 1)
        {
            //Need a room with a Up door

            if (rooms < 20)
                r = Instantiate(rt.uRooms[Random.Range(0, rt.uRooms.Length)], transform.position, Quaternion.identity); //randomly
            else
                r = Instantiate(rt.uRoom, transform.position, Quaternion.identity); //No more exits
        }
        else if (placement == 2)
        {
            //Need a room with a Right door

            if (rooms < 20)
                r = Instantiate(rt.rRooms[Random.Range(0, rt.rRooms.Length)], transform.position, Quaternion.identity); //randomly
            else
                r = Instantiate(rt.rRoom, transform.position, Quaternion.identity); //No more exits
        }
        else if (placement == 3)
        {
            //Need a room with a Left door

            if (rooms < 20)
                r = Instantiate(rt.lRooms[Random.Range(0, rt.lRooms.Length)], transform.position, Quaternion.identity); //randomly
            else
                r = Instantiate(rt.lRoom, transform.position, Quaternion.identity); //No more exits
        }

        rooms++;

        rt.roomsSpawned.Add(r);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("SpawnPoints"))
            Destroy(gameObject); //If I encounter another point or a room , destroy myself
    }
}
