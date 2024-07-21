using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    [SerializeField] GameObject[] menuObjects;
    public DataQueue dataQueue;
    bool isOpen = false;

    void OnAwake(){
        foreach(GameObject obj in menuObjects){
            obj.SetActive(false);
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B) && !isOpen){
            foreach(GameObject obj in menuObjects){
                obj.SetActive(true);
            }
            dataQueue.ReapplyData();
            isOpen = true;
        }else if(Input.GetKeyDown(KeyCode.B) && isOpen){
            foreach(GameObject obj in menuObjects){
                obj.SetActive(false);
            }
            isOpen = false;
        }
    }
}
