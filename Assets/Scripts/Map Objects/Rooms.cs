using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    private Transform player;
    private CinemachineVirtualCamera vcam;

    void Awake(){
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
    }

    void Update(){
        if(GameObject.FindGameObjectWithTag("Player")){
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Player") vcam.Follow = transform;
    }
}
