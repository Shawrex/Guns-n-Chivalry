using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPurchasing : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite paquerette = null;
    [SerializeField] private Sprite cactus = null;
    [SerializeField] private Sprite rose = null;

    [Header("Prefabs")]
    [SerializeField] private GameObject paquerettePrefab = null;
    [SerializeField] private GameObject cactusPrefab = null;
    [SerializeField] private GameObject rosePrefab = null;

    [Header("Misc")]
    [SerializeField] private GameObject cursorObject = null;
    [SerializeField] private GameObject playerRange = null;
    [SerializeField] private float distanceToPlayer = 0f;

    private SpriteRenderer cursorObjectRenderer;
    private GameObject player;
    public List<GameObject> plants;
    public static TowerPurchasing instance;
    [NonSerialized] public bool placeable = true;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 10);

        cursorObjectRenderer = cursorObject.GetComponent<SpriteRenderer>();

        player = GameObject.Find("Player");
    }

    public void BuyTower(string towerName)
    {
        if (towerName == "paquerette" && ScoreScript.instance.money >= Paquerette.price)
        {
            cursorObjectRenderer.sprite = paquerette;
            playerRange.SetActive(true);
        }
        else if (towerName == "cactus" && ScoreScript.instance.money >= Cactus.price)
        {
            cursorObjectRenderer.sprite = cactus;
            playerRange.SetActive(true);
        }
        else if (towerName == "rose" && ScoreScript.instance.money >= Rose.price)
        {
            cursorObjectRenderer.sprite = rose;
            playerRange.SetActive(true);
        }
    }

    private void Update()
    {
        if (cursorObject.activeSelf)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 offsetPos = mousePos - (Vector2)player.transform.position;
            
            if (offsetPos.magnitude <= distanceToPlayer)
                cursorObject.transform.position = (Vector2)player.transform.position + offsetPos;
            else
                cursorObject.transform.position = (Vector2)player.transform.position + offsetPos.normalized * distanceToPlayer;


            if (!placeable)
                cursorObjectRenderer.color = Color.red;
            else
                cursorObjectRenderer.color = Color.white;


            if (Input.GetKeyDown(KeyCode.Mouse0) && placeable)
            {
                if (cursorObjectRenderer.sprite == paquerette)
                {
                    GameObject p = Instantiate(paquerettePrefab, cursorObject.transform.position, Quaternion.identity);
                    ScoreScript.instance.ChangeScore("money", -Paquerette.price);
                    plants.Add(p);
                }
                else if (cursorObjectRenderer.sprite == cactus)
                {
                    GameObject p = Instantiate(cactusPrefab, cursorObject.transform.position, Quaternion.identity);
                    ScoreScript.instance.ChangeScore("money", -Cactus.price);
                    plants.Add(p);
                }
                else if (cursorObjectRenderer.sprite == rose)
                {
                    GameObject p = Instantiate(rosePrefab, cursorObject.transform.position, Quaternion.identity);
                    ScoreScript.instance.ChangeScore("money", -Rose.price);
                    plants.Add(p);
                }

                cursorObjectRenderer.sprite = null;
                playerRange.SetActive(false);
            }


            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                cursorObjectRenderer.sprite = null;
                playerRange.SetActive(false);
            }
        }
    }
}
