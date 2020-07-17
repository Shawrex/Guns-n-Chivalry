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
}
