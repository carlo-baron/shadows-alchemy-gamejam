using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]CanvasGroup[] canvasGroups;
    Button quitButton;
    [SerializeField] float fadeSpeed;
    void Awake(){
        if(canvasGroups != null){
            quitButton = canvasGroups[1].gameObject.GetComponent<Button>();
            foreach(CanvasGroup canvas in canvasGroups){
                StartCoroutine(FadeIn(canvas));
            }
        }
    }

    IEnumerator FadeIn(CanvasGroup canvas){
        while(canvas.alpha < 1){
            canvas.alpha += 0.1f;
            yield return new WaitForSeconds(fadeSpeed);
        }
    }
    public void StartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndGame(){
        Application.Quit();
    }
}
