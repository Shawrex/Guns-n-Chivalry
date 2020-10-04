using static UnityEngine.Physics2D;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class PlantsManager : MonoBehaviour
{
    public static PlantsManager inst;

    private bool placingPlant = false;
    bool locked = false;

    [SerializeField] private List<GameObject> plantPrefabs = null;
    private GameObject heldPlant = null;

    [SerializeField] private List<GameObject> activePlants = null;
    private GameObject selectedPlant;

    private void Start() => inst = this;

    void Update()
    {
        if (!placingPlant)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                SelectPlant();
            else if (Input.GetKeyDown(KeyCode.Mouse1) && selectedPlant != null)
                DeletePlant();
        }
        else
        {
            MoveHeldPlant();

            if (Input.GetKeyDown(KeyCode.Mouse0))
                PlacePlant();
        }
    }



    /// FUNCTIONS
    
    private void SelectPlant()
    {
        selectedPlant = null;
        Collider2D clickedObject = OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);

        foreach (var p in activePlants)
        {
            if (clickedObject != null && p == clickedObject.gameObject)
            {
                selectedPlant = p;
                selectedPlant.GetComponent<IPlantsInterface>().Select(true);
            }
            else
                p.GetComponent<IPlantsInterface>().Select(false);
        }
    }

    private void DeletePlant()
    {
        Collider2D[] clickedObjects = OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
        foreach (Collider2D o in clickedObjects)
        {
            if (o.gameObject == selectedPlant)
            {
                InventoryManager.inst.AddInventory(selectedPlant);
                activePlants.Remove(selectedPlant);
                Destroy(selectedPlant);
            }
        }
    }

    public void HoldPlant(int plantType, int plantStage) 
    {
        placingPlant = true;
        heldPlant = Instantiate(plantPrefabs[plantType], Vector2.zero, Quaternion.identity);
        for (int i = 0; i < plantStage; i++)
        {
            heldPlant.GetComponent<IPlantsInterface>().LevelUp();
        }
    }

    private void MoveHeldPlant()
    {
        locked = false;

        Collider2D[] hoveredObjects = OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
        if (hoveredObjects.Length > 0)
        {
            foreach (Collider2D ho in hoveredObjects)
            {
                if (ho.gameObject.CompareTag("Farmcell"))
                {
                    heldPlant.transform.position = (Vector2)ho.transform.position;
                    locked = true;
                    break;
                }
            }

            if (locked == false)
                heldPlant.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            heldPlant.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void PlacePlant()
    {
        placingPlant = false;

        activePlants.Add(heldPlant);

        heldPlant.GetComponent<SpriteRenderer>().color = Color.white;

        if (locked == true)
            StartCoroutine(heldPlant.GetComponent<IPlantsInterface>().Grow());

        heldPlant = null;
    }
}

public interface IPlantsInterface
{
    void Select(bool isSelected);
    IEnumerator Grow();
    void LevelUp();
    int GetType();
    int GetStage();
}

