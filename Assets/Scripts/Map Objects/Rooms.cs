using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    private RespawnManager respawnManager;

    void Awake(){
        
        respawnManager = GameObject.FindObjectOfType<RespawnManager>();
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Player"){
            respawnManager.currentRespawnPoint = transform.GetChild(0);
        }
    }
}
