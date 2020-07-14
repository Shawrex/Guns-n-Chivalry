﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsTemplates : MonoBehaviour
{
    public GameObject[] uRooms, dRooms, lRooms,  rRooms; //Rooms with Up , Down , Left , Right doors to spawn;
    public GameObject uRoom, dRoom, lRoom, rRoom; //Rooms used to stop spawning more and close the dungeon;

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
}
