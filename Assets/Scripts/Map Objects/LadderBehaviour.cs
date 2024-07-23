using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderBehaviour : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Transform player;
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if(transform.position.y < player.position.y){
            boxCollider.isTrigger = false;
        }else if(transform.position.y > player.position.y){
            boxCollider.isTrigger = transform;
        }
    }
}
