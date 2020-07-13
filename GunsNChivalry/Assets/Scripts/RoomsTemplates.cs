using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsTemplates : MonoBehaviour
{
    public GameObject[] uRooms, dRooms, lRooms,  rRooms; //Rooms with Up , Down , Left , Right doors to spawn;
    public GameObject uRoom, dRoom, lRoom, rRoom; //Rooms used to stop spawning more and close the dungeon;

    [Header("Do not put anything here")]
    public List<GameObject> roomsSpawned; //List of rooms spawned

    public static RoomsTemplates instance; //This

    private void Start()
    {
        if (instance == null)
            instance = this; //Set it to the instance to get refered easily

        Invoke("Exit", 2f); //2 seconds after start I call Exit()
    }

    private void Exit()
    {
        roomsSpawned[roomsSpawned.Count - 1].GetComponent<SpriteRenderer>().color = Color.red; //Turn the last room spawned to red ( boss )
    }
}
