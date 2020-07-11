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

    private void Start()
    {
        cursorObjectRenderer = cursorObject.GetComponent<SpriteRenderer>();
        cursorObject.SetActive(false);
    }

    public void BuyTower(string towerName)
    {
        if (towerName == "knight")
            cursorObjectRenderer.sprite = knight;

        cursorObject.SetActive(true);
    }

    private void Update()
    {
        if (cursorObject.activeSelf)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorObject.transform.position = mousePos;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (cursorObjectRenderer.sprite == knight && ScoreScript.instance.score >= 5)
                {
                    ScoreScript.instance.ChangeScore(-5);
                    Instantiate(knightPrefab, cursorObject.transform.position, Quaternion.identity);
                }

                cursorObject.SetActive(false);
            }
        }
    }
}
