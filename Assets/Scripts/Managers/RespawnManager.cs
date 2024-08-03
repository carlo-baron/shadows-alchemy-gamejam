using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform currentRespawnPoint;
    private CinemachineVirtualCamera vcam;
    private GameObject playerTracker;
    [SerializeField] GameObject playerPrefab;
    void Awake()
    {
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        playerTracker = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        if(playerTracker == null){
            Respawn();
        }
    }

    void Respawn(){
        GameObject player = Instantiate(playerPrefab, currentRespawnPoint.position, Quaternion.identity);
        player.GetComponent<AbilityUnlocker>().numberOfAbilities = GameManager.abilities;
        playerTracker = GameObject.FindGameObjectWithTag("Player");
    }
}
