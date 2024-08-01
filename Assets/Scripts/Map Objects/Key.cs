using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    Vector2 startPos;
    Transform followTransform; 
    float speed;
    [SerializeField, Range(0,1)] float speedMultiplier;
    bool collected = false;
    bool canOpenDoor = false;
    GameObject door;
    Player player;

    void Awake(){
        player = GameObject.FindObjectOfType<Player>();
        startPos = transform.position;
        speed = player.runSpeed * speedMultiplier;
    }

    void Update()
    {
        if(player == null){
            player = GameObject.FindObjectOfType<Player>();
        }else{
            speed = player.runSpeed * speedMultiplier;
        }

        float step = speed * Time.deltaTime;

        if(followTransform != null){
            transform.position = Vector2.MoveTowards(transform.position, followTransform.position, step);
        }else{
            transform.position = startPos;
        }

        if(canOpenDoor) OpenDoor();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" && !collected){
            followTransform = other.gameObject.transform.GetChild(1).transform;
            collected = true;
        }else if(other.tag == "door" && collected){
            followTransform = other.gameObject.transform;
            door = other.gameObject;
            canOpenDoor = true;
        }
    }

    void OpenDoor(){
        if(collected && transform.position == followTransform.position){
            door.GetComponent<Animator>().SetTrigger("open");
            door.GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);
        }
    }
}
