using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemExchange : MonoBehaviour, IPointerClickHandler
{
    ItemScriptable myData;
    InventoryManager inventoryManager;
    CraftingManager craftingManager;

    void Awake(){
        inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
        craftingManager = GameObject.FindObjectOfType<CraftingManager>();
    }


    void Start(){
        myData = GetComponent<ItemsDraggable>().item;
    }

    public void OnPointerClick(PointerEventData eventData){
        if(myData.ability == true){
            inventoryManager.AddItem(myData);
            craftingManager.DestoryOffers();
            Destroy(gameObject);
        }
    }

}
