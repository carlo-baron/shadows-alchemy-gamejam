using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Itemslots[] slots { get; private set; }
    public GameObject itemsTemplate;
    public DataQueue dataQueue;
    GameObject slotsContainer;
    void Awake()
    {
        slotsContainer = transform.GetChild(0).gameObject;
        slots = slotsContainer.GetComponentsInChildren<Itemslots>();
    }

    public void AddItem(ItemScriptable item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Itemslots slot = slots[i];
            ItemsDraggable itemInSlot = slot.GetComponentInChildren<ItemsDraggable>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return;
            }
        }
    }

    public void SpawnNewItem(ItemScriptable item, Itemslots slot)
    {
        GameObject newItem = Instantiate(itemsTemplate, slot.transform);
        ItemsDraggable itemScript = newItem.GetComponent<ItemsDraggable>();
        if (newItem.activeInHierarchy)
        {
            itemScript.InitializeItem(item);
        }
        else
        {
            dataQueue.AddToQueue(item);
        }
    }
}
