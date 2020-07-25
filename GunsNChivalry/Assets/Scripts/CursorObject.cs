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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            List<Collider2D> touched;
            touched = new List<Collider2D>(Physics2D.OverlapCircleAll(transform.position, 0.25f));
            
            foreach (GameObject t in TowerPurchasing.instance.turrets)
                t.GetComponentInChildren<ShootingTargeting>().Unselected();

            if (touched.Count > 1)
            {
                if (touched[0].name == gameObject.name)
                    touched.RemoveAt(0);

                if (touched[0].CompareTag("Turret"))
                    touched[0].GetComponentInChildren<ShootingTargeting>().Selected();
            }
        }
    }
}
