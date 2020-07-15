using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public bool spawned = false;
    private int placement; //0 = U , 1 = D, 2 = L, 3 = R
    private GameObject roomToSpawn; //The room template I'll spawn
    private RoomsTemplates roomsTemplates; //All the rooms templates
    private GameObject myRoom; //The room tspawned;

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

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        roomsTemplates = RoomsTemplates.instance; //Get all the templates

        yield return new WaitForSeconds(0.1f);

        if (roomsTemplates.roomsSpawned.Count < 20 && !spawned)
        {
            switch (placement)
            {
                case 0: //Need to spawn a D room
                    roomToSpawn = roomsTemplates.dRooms[Random.Range(0, roomsTemplates.dRooms.Length)];
                    break;
                case 1: //Need to spawn a U room
                    roomToSpawn = roomsTemplates.uRooms[Random.Range(0, roomsTemplates.dRooms.Length)];
                    break;
                case 2: //Need to spawn a R room
                    roomToSpawn = roomsTemplates.rRooms[Random.Range(0, roomsTemplates.dRooms.Length)];
                    break;
                case 3: //Need to spawn a L room
                    roomToSpawn = roomsTemplates.lRooms[Random.Range(0, roomsTemplates.dRooms.Length)];
                    break;
            }

            //Spawn my room
            myRoom = Instantiate(roomToSpawn, transform.position, Quaternion.identity);
            spawned = true;

            //Add my room to the list of rooms
            roomsTemplates.roomsSpawned.Add(myRoom);
        }
        else if (!spawned)
        {
            switch (placement)
            {
                case 0: //Need to spawn a D room CLOSED
                    roomToSpawn = roomsTemplates.dRoom;
                    break;
                case 1: //Need to spawn a U room CLOSED
                    roomToSpawn = roomsTemplates.uRoom;
                    break;
                case 2: //Need to spawn a R room CLOSED
                    roomToSpawn = roomsTemplates.rRoom;
                    break;
                case 3: //Need to spawn a L room CLOSED
                    roomToSpawn = roomsTemplates.lRoom;
                    break;
            }

            //Spawn my room
            myRoom = Instantiate(roomToSpawn, transform.position, Quaternion.identity);
            spawned = true;

            //Add my room to the list of rooms
            roomsTemplates.roomsSpawned.Add(myRoom);
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.CompareTag("Room"))
            Destroy(gameObject); //Destroy to cancel room flood
        else if (c.CompareTag("SpawnPoints"))
        {
            RoomSpawner other = c.GetComponent<RoomSpawner>();

            if (!other.spawned && !spawned)
            {
                spawned = true;
                Destroy(gameObject, 2f);
            }
        }
    }
}
