using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// if ever saving and loading is needed
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }

    }

    void Update()
    {
        
    }
}
