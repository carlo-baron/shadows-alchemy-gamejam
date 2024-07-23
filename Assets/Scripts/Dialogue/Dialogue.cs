using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] lines;
    [SerializeField]float textSpeed;
    private int index;
    void Start()
    {
        text.text = string.Empty;
    }

    void Update()
    {
        if(Input.GetMouseButton(0)){
            if(text.text == lines[index]){
                NextLine();
            }else{
                StopAllCoroutines();
                text.text = lines[index];
            }
        }
    }

    void StartDialogue(){
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach(char character in lines[index].ToCharArray()){
            text.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(){
        if(index < lines.Length - 1){
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }else{
            gameObject.SetActive(false);
        }
    }
}
