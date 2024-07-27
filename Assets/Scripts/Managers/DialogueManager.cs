using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    private string[] lines;
    [SerializeField] float textSpeed;
    private int index;
    private bool inDialogue = false;
    void Start()
    {
        text.text = string.Empty;
    }

    void Update()
    {
        if(inDialogue){
            if (Input.GetMouseButtonDown(0))
            {
                if (text.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    text.text = lines[index];
                }
            }
        }
    }

    public void StartDialogue(string[] lines)
    {
        this.lines = lines;
        inDialogue = true;
        transform.GetChild(0).gameObject.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char character in lines[index].ToCharArray())
        {
            text.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            inDialogue = false;
        }
    }
}
