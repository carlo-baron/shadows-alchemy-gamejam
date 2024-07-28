using System.Collections;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    [Header("Content/s")]
    [SerializeField] ItemScriptable[] items;
    [SerializeField] GameObject worldItemtemplate;

    [Header("Spawning Contents")]
    [SerializeField] float maxSpawnOffset = 0.5f;
    [SerializeField] float spawnSpeed;
    [SerializeField] float spawnDelay;

    [Header("Control Hint")]
    [SerializeField] float fadeSpeed;
    ControlHints controlHints;

    Transform itemSpawnPoint;
    Animator anim;
    public bool isOpen { get; private set; }
    private bool isSpawning;

    void Awake()
    {
        anim = GetComponent<Animator>();
        controlHints = GetComponentInChildren<ControlHints>();
        itemSpawnPoint = GetComponentInChildren<Transform>();

        isOpen = false;
        isSpawning = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpen)
            controlHints.Show(fadeSpeed);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F) && !isSpawning)
            {
                if (items != null && !isOpen)
                {
                    controlHints.Hide(fadeSpeed);
                    StartCoroutine(DelayedSpawn());
                    anim.SetTrigger("open");
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            controlHints.StopAllCoroutines();
            controlHints.Hide(fadeSpeed);
    }

    IEnumerator DelayedSpawn()
    {
        isSpawning = true;
        yield return new WaitForSeconds(spawnDelay);

        if (!isOpen)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Vector2 spawnLocation = new Vector2(itemSpawnPoint.position.x + Random.Range(-maxSpawnOffset, maxSpawnOffset), itemSpawnPoint.position.y + .5f);
                GameObject item = Instantiate(worldItemtemplate, spawnLocation, Quaternion.identity);
                WorldItems itemScript = item.GetComponent<WorldItems>();
                itemScript.itemData = items[i];
                itemScript.spriteRenderer.sprite = items[i].sprite;
                itemScript.spawnSpeed = spawnSpeed;
            }
            isOpen = true;
            this.enabled = false;
        }
        isSpawning = false;
    }
}
