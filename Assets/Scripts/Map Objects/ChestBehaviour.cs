using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    [Header("Content/s")]
    [SerializeField] ItemScriptable[] items;
    [SerializeField] GameObject worldItemtemplate;

    [SerializeField] float maxSpawnOffset = 0.5f;
    [SerializeField] float spawnDelay;

    Transform itemSpawnPoint;
    BoxCollider2D boxCollider;
    Animator anim;
    bool isOpen = false;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        itemSpawnPoint = GetComponentInChildren<Transform>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.F) && other.tag == "Player")
        {
            if (items != null)
            {
                Invoke("DelayedSpawn", spawnDelay);
            }
            anim.SetTrigger("open");
        }
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