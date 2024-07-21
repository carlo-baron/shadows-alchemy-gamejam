using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItems : MonoBehaviour
{
    public ItemScriptable itemData;
    public InventoryManager inventoryManager;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.sprite;
        gameObject.name = itemData.name;
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            inventoryManager.AddItem(itemData);
            gameObject.SetActive(false);
        }
    }

}
