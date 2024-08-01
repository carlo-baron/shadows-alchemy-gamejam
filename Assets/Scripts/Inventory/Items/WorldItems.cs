using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItems : MonoBehaviour
{
    public float spawnSpeed { get; set; }

    public ItemScriptable itemData;
    private InventoryManager inventoryManager;
    public SpriteRenderer spriteRenderer;
    Collider2D myCollider;
    Rigidbody2D rb;
    bool stopUpdate = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    void Start()
    {
        rb.velocity = Vector2.up * spawnSpeed;
        myCollider.enabled = false;
        if (itemData != null)
        {
            gameObject.name = itemData.name;
            spriteRenderer.sprite = itemData.sprite;
        }
    }

    void Update()
    {
        if(!stopUpdate){
            if (rb.velocity.y > 0)
            {
                myCollider.enabled = false;
            }
            else if (rb.velocity.y < 0)
            {
                myCollider.enabled = true;
                stopUpdate = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inventoryManager.AddItem(itemData);
            gameObject.SetActive(false);
        }
    }

}
