using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    private Transform currentRespawnPoint;
    private CinemachineVirtualCamera vcam;
    private GameObject playerTracker;
    [SerializeField] GameObject playerPrefab;
    void Awake()
    {
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        if(vcam.Follow != null){
            currentRespawnPoint = vcam.Follow.transform.GetChild(0).GetComponent<Transform>();
        }

        playerTracker = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(playerTracker == null){
            Respawn();
        }
    }

    void Respawn(){
        Instantiate(playerPrefab, currentRespawnPoint.position, Quaternion.identity);
        playerTracker = GameObject.FindGameObjectWithTag("Player");
    }
}
