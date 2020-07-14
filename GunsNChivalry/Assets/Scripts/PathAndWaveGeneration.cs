﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAndWaveGeneration : MonoBehaviour
{
    [SerializeField] private int pathNumbers = 0;
    public List<Transform> roomPath = null;
    public List<GameObject> roomPathRender = null;
    private Vector2 pos;
    [SerializeField] private GameObject pathPoint = null;
    [SerializeField] private GameObject pathRender = null;

    void Start()
    {
        Transform[] children = GetComponentsInChildren<Transform>();

        for (int i = 0; i < pathNumbers; i++)
        {
            if (i == 0 || i == pathNumbers - 1)
            {
                pos = children[Random.Range(1, children.Length)].position.normalized;
                pos = new Vector2(pos.x * 9.1f, pos.y * 5.1f);
            }
            else
                pos = new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 4f));


            GameObject p = Instantiate(pathPoint, transform);
            p.transform.localPosition = pos;
            roomPath.Add(p.transform);
        }

        for (int i = 0; i < pathNumbers - 1; i++)
        {
            Vector2 pos1 = roomPath[i].localPosition;
            Vector2 pos2 = roomPath[i + 1].localPosition;

            GameObject r = Instantiate(pathRender, transform);
            r.transform.localPosition = (pos1 + pos2) / 2f;
            r.transform.right = pos1 - pos2;
            r.transform.localScale = new Vector3(Vector2.Distance(pos1, pos2), 0.1f, 1f);
            Vector3 color = new Vector3(1f / pathNumbers * (i + 1f), 1f / (i + 1f), 0f).normalized * 1.5f;
            r.GetComponent<SpriteRenderer>().color = new Color(color.x , color.y, color.z);
            roomPathRender.Add(r);
        }
    }
}