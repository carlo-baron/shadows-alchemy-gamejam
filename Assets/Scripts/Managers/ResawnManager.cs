using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ResawnManager : MonoBehaviour
{
    private Transform currentRespawnPoint;
    private CinemachineVirtualCamera vcam;
    private GameObject playerTracker;
    [SerializeField] GameObject playerPrefab;
    void Awake()
    {
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        if(vcam.LookAt != null) currentRespawnPoint = vcam.LookAt.transform;
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
