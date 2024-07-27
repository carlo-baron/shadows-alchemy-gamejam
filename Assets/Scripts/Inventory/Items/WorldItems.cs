using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class WorldItems : MonoBehaviour
{
    [SerializeField]float spawnSpeed;

    public ItemScriptable itemData;
    private InventoryManager inventoryManager;
    public SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;
    Rigidbody2D rb;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();

    }

    void Start(){
        rb.AddForce(Vector2.up * spawnSpeed, ForceMode2D.Impulse);
        circleCollider.enabled = false;
        if(itemData != null){
            gameObject.name = itemData.name;
            spriteRenderer.sprite = itemData.sprite;
        }
    }

    void Update(){
        if(rb.velocity.y > 0){
            circleCollider.enabled = false;
        }else if(rb.velocity.y < 0){
            circleCollider.enabled = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            inventoryManager.AddItem(itemData);
            gameObject.SetActive(false);
        }
    }

}
