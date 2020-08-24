using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObject : MonoBehaviour
{
    private TowerPurchasing tp;

    //Get the instance of the TowerPurchasing script
    private void Start() => tp = TowerPurchasing.instance;

    private void OnTriggerEnter2D(Collider2D c) => tp.placeable = false;
    private void OnTriggerStay2D(Collider2D c) => tp.placeable = false;
    private void OnTriggerExit2D(Collider2D c) => tp.placeable = true;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Collider2D[] clickedObjects = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);

            foreach (GameObject p in TowerPurchasing.instance.plants)
                p.GetComponent<PlantSelection>().Unselect();

            foreach (Collider2D o in clickedObjects)
            {
                if (o.gameObject.GetComponent<PlantSelection>() != null)
                {
                    o.gameObject.GetComponent<PlantSelection>().Select();

                    break;
                }
            }
        }
    }

}
