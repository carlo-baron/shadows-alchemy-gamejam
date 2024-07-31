using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemExchange : MonoBehaviour, IPointerClickHandler
{
    ItemScriptable myData;
    InventoryManager inventoryManager;
    CraftingManager craftingManager;
    AbilityUnlocker abilityUnlocker;

    void Awake(){
        inventoryManager = GameObject.FindObjectOfType<InventoryManager>();
        craftingManager = GameObject.FindObjectOfType<CraftingManager>();
        abilityUnlocker = GameObject.FindObjectOfType<AbilityUnlocker>();
    }


    void Start(){
        myData = GetComponent<ItemsDraggable>().item;
    }

    public void OnPointerClick(PointerEventData eventData){
        if(myData.ability == true){
            inventoryManager.AddItem(myData);
            craftingManager.DestoryOffers();
            abilityUnlocker.numberOfAbilities++;
            abilityUnlocker.SetAbilities();
            Destroy(gameObject);
        }
    }

}
