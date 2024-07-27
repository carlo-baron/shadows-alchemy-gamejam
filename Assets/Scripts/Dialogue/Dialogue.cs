using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public string[] Lines{ get{ return lines; } private set{ lines = value; }}
    [SerializeField]private string[] lines;
    DialogueManager dialogueManager;

    void Awake(){
        dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
    }

    void PassDialogue(){
        dialogueManager.StartDialogue(lines);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            PassDialogue();
        }
    }
}
