using System.Runtime.CompilerServices;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    [Header("Content/s")]
    [SerializeField] ItemScriptable[] items;
    [SerializeField] GameObject worldItemtemplate;

    [Header("Spawning Contents")]
    [SerializeField] float maxSpawnOffset = 0.5f;
    [SerializeField] float spawnDelay;

    [Header("Control Hint")]
    [SerializeField] float fadeSpeed;
    ControlHints controlHints;

    Transform itemSpawnPoint;
    BoxCollider2D boxCollider;
    Animator anim;
    bool isOpen = false;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        controlHints = GetComponentInChildren<ControlHints>();

        itemSpawnPoint = GetComponentInChildren<Transform>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player"){
            
            controlHints.Show(fadeSpeed, 0);

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (items != null)
                {
                    Invoke("DelayedSpawn", spawnDelay);
                }
                anim.SetTrigger("open");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        controlHints.Hide(fadeSpeed, 1);
    }

    void DelayedSpawn()
    {
        if(!isOpen){
            for (int i = 0; i < items.Length; i++)
            {
                Vector2 spawnLocation = new Vector2(itemSpawnPoint.position.x + Random.Range(-maxSpawnOffset, maxSpawnOffset), itemSpawnPoint.position.y);
                GameObject item = Instantiate(worldItemtemplate, spawnLocation, Quaternion.identity);
                WorldItems itemScript = item.GetComponent<WorldItems>();
                itemScript.itemData = items[i];
                itemScript.spriteRenderer.sprite = items[i].sprite;
            }
            isOpen = true;
            this.enabled = false;
        }
    }
}