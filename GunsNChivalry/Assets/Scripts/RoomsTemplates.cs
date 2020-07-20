using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsTemplates : MonoBehaviour
{
    public int currentRoom = 0;

    public GameObject[] uRooms, dRooms, lRooms,  rRooms; //Rooms with Up , Down , Left , Right doors to spawn;
    public GameObject uRoom, dRoom, lRoom, rRoom, cRoom; //Rooms used to stop spawning more and close the dungeon;

    public List<GameObject> roomsSpawned; //List of rooms spawned

    [NonSerialized] public static RoomsTemplates instance; //This

    private void Awake()
    {
        //Set an instance to get the vars easily
        if (instance == null)
            instance = this;

        //Call the "Exit()" 2s after
        Invoke("Exit", 2f); 
    }

    private void Exit()
    {
        //Turn the last room spawned to red ( later in a boss )
        roomsSpawned[roomsSpawned.Count - 1].GetComponent<SpriteRenderer>().color = Color.red;
        
        //Destroy all rooms triggers
        foreach (GameObject r in roomsSpawned)
        {
            r.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void PlayWave()
    {
        foreach (GameObject r in roomsSpawned)
            r.GetComponents<PolygonCollider2D>()[1].isTrigger = false;

        roomsSpawned[currentRoom].GetComponent<PathAndWaveGeneration>().PlayWave();
    }

    public void EnteredRoom()
    {
        foreach (GameObject r in roomsSpawned)
            r.GetComponent<PathAndWaveGeneration>().Exited();

        roomsSpawned[currentRoom].GetComponent<PathAndWaveGeneration>().Entered();
    }
}
