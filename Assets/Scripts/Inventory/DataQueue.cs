using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataQueue : MonoBehaviour
{
    public InventoryManager inventoryManager;
    Queue<ItemScriptable> itemScriptables = new Queue<ItemScriptable>();
    public void AddToQueue(ItemScriptable item){
        itemScriptables.Enqueue(item);
    }
    public void ReapplyData(){
        if(itemScriptables.Count > 0){
            foreach(Itemslots slot in inventoryManager.slots){
                ItemsDraggable itemChild = slot.GetComponentInChildren<ItemsDraggable>();
                if(itemChild != null && !itemChild.isInitialized){
                    itemChild.InitializeItem(itemScriptables.Dequeue());
                }
            }
        }
    }   
}
