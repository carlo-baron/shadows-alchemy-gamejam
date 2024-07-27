using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    GameObject[] menuObjects;
    private UIManager UImanager;
    private DataQueue dataQueue;
    bool isOpen = false;

    void Awake(){
        dataQueue = GameObject.FindObjectOfType<DataQueue>();
        UImanager = GameObject.FindObjectOfType<UIManager>();
        menuObjects = new GameObject[UImanager.hiddenUI.Length];

        for(int i = 0; i < menuObjects.Length; i++){
            menuObjects[i] = UImanager.hiddenUI[i];
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
