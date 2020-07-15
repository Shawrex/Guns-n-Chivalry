using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPurchasing : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite knight = null;

    [Header("Prefabs")]
    [SerializeField] private GameObject knightPrefab = null;

    [Header("Misc")]
    [SerializeField] private GameObject cursorObject = null;
    private SpriteRenderer cursorObjectRenderer;
    [NonSerialized] public static TowerPurchasing instance;
    [NonSerialized] public bool placeable = true;

    private void Awake()
    {
        //Set the instance to get this script later
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(0, 10);
        Physics2D.IgnoreLayerCollision(9, 10);

        //Create an object that will follow the mouse BUT desactivate it
        cursorObjectRenderer = cursorObject.GetComponent<SpriteRenderer>();
        cursorObject.SetActive(false);
    }

    public void BuyTower(string towerName)
    {
        //When i click on a button and i have the money for it
        if (towerName == "knight" && ScoreScript.instance.score >= 5)
        {
            //Set the sprite of the object that follows mouse to the knight and activate it
            cursorObjectRenderer.sprite = knight;
            cursorObject.SetActive(true);
        }
    }

    private void Update()
    {
        //Is the object that follows the mouse is active (Player is buying something)
        if (cursorObject.activeSelf)
        {
            //Move it to the mouse each frame
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorObject.transform.position = mousePos;

            //If the tower cant be placed then turn it red
            if (!placeable)
                cursorObjectRenderer.color = Color.red;
            else
                cursorObjectRenderer.color = Color.white;

            //If I left click and I can place it
            if (Input.GetKeyDown(KeyCode.Mouse0) && placeable)
            {
                //Check the tower type

                if (cursorObjectRenderer.sprite == knight)
                {
                    //Change the "score" (money) AND create a knight on the map
                    ScoreScript.instance.ChangeScore(-5);
                    Instantiate(knightPrefab, cursorObject.transform.position, Quaternion.identity);
                }

                //Desacitvate the object
                cursorObject.SetActive(false);
            }

            //If I right click
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //Desactivate the object to cancel the buy
                cursorObject.SetActive(false);
            }
        }
    }
}
