using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotatingLamp : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] LayerMask playerLayer;

    private Transform player;
    private float rotationFactor;
    

    bool playerDetected;

    void Awake()
    {
        playerDetected = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player")){
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        if (!playerDetected)
        {
            rotationFactor = Time.deltaTime * rotationSpeed;
            transform.Rotate(new Vector3(0, 0, rotationFactor));
        }
        else
        {
            Vector2 difference = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RaycastHit2D ray = Physics2D.Linecast(transform.position, player.position, playerLayer);
            if(ray.collider != null){
                playerDetected = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {

        playerDetected = false;
    }


}
