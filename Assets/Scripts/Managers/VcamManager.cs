using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VcamManager : MonoBehaviour
{
    CinemachineVirtualCamera myVcam;
    CinemachineFramingTransposer myVcamValues;
    CinemachineVirtualCameraBase myVcamBase;
    Transform player;
    float moveInput;


    void Awake()
    {
        myVcam = GetComponent<CinemachineVirtualCamera>();
        myVcamValues = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();

        //temp
        if(myVcam.Follow != null){
            player = myVcam.Follow.transform;
        }
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if(moveInput > 0){
            myVcamValues.m_ScreenX = 0.4f;
        }else if(moveInput < 0){
            myVcamValues.m_ScreenX = 0.6f;
        }
    }
}
