using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem
{
    public void ItemData(int _slot, int _type, int _level)
    {
        slot = _slot;
        type = _type;
        level = _level;
    }

    public int slot;
    public int type;
    public int level;
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager inst;
    [SerializeField] private Sprite defautSprite = null;

    private void Start() => inst = this;

    public List<Image> slots = null;
    readonly List<InventoryItem> items = new List<InventoryItem>();

    public void AddInventory(GameObject plantAdd)
    {
        Sprite itemSprite = plantAdd.GetComponent<SpriteRenderer>().sprite;
        slots[items.Count].sprite = itemSprite;

        InventoryItem item = new InventoryItem();
        item.ItemData(
            items.Count, 
            plantAdd.GetComponent<IPlantsInterface>().GetType(), 
            plantAdd.GetComponent<IPlantsInterface>().GetStage()
        );
        items.Add(item);

        foreach (MonoBehaviour s in plantAdd.GetComponents<MonoBehaviour>())
            s.StopAllCoroutines();
    }

    public void RemoveInventory(int slot)
    {
        if (slots[slot].sprite != defautSprite)
        {
            PlantsManager.inst.HoldPlant(items[slot].type, items[slot].level);
            items.RemoveAt(slot);
            slots[slot].sprite = defautSprite;
        }
    }
}

