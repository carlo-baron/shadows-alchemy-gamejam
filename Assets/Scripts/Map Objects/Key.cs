using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    Vector2 startPos;
    Transform followTransform; 
    [SerializeField] float speed;
    bool collected = false;
    bool canOpenDoor = false;
    GameObject door;

    void Awake(){
        startPos = transform.position;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        if(followTransform != null){
            transform.position = Vector2.MoveTowards(transform.position, followTransform.position, step);
        }else{
            transform.position = startPos;
            collected = false;
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
