using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject itemsInventory;
    [SerializeField] GameObject hotbar;

    public Itemslots[] itemSlots { get; private set; }
    public Itemslots[] abilitySlots { get; private set; }
    public GameObject itemsTemplate;
    public DataQueue dataQueue;
    void Awake()
    {
        itemSlots = itemsInventory.GetComponentsInChildren<Itemslots>();
        abilitySlots = hotbar.GetComponentsInChildren<Itemslots>();

    }

    public void AddItem(ItemScriptable item)
    {
        Itemslots[] currentSlots;
        if(!item.ability){
            currentSlots = itemSlots;
        }else{
            currentSlots = abilitySlots;
        }

        for (int i = 0; i < currentSlots.Length; i++)
        {
            Itemslots slot = currentSlots[i];
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
        if(item.ability){
            newItem.transform.Rotate(0, 0, -90);
            newItem.GetComponent<ItemsDraggable>().enabled = false;
            newItem.GetComponent<ItemExchange>().enabled = false;
        }
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
