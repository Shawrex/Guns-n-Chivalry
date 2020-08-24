using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSelection : MonoBehaviour
{
    [SerializeField] private GameObject rangeObject = null;

    public void Select() => rangeObject.SetActive(true);

    public void Unselect() => rangeObject.SetActive(false);
}
