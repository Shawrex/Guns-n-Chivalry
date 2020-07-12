using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsTemplates : MonoBehaviour
{
    public GameObject[] uRooms;
    public GameObject[] dRooms;
    public GameObject[] lRooms;
    public GameObject[] rRooms;

    public static RoomsTemplates instance;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }
}
