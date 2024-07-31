using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VcamManager : MonoBehaviour
{
    private CinemachineVirtualCamera myVcam;
    public bool isDead = false;
    Transform player;


    void Awake()
    {
        myVcam = GetComponent<CinemachineVirtualCamera>();
        if(myVcam.Follow != null){
            player = myVcam.Follow.transform;
        }
    }

    void Update()
    {
        if(!isDead){
            if(GameObject.FindGameObjectWithTag("Player") != null){
                myVcam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }else{
            myVcam.Follow = null;
        }

    }
}
