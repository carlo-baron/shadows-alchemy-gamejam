using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlHints : MonoBehaviour
{
    [SerializeField] Sprite sprite;
    private Image hintImage;
    private CanvasGroup canvasGroup;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        hintImage = GetComponent<Image>();
        hintImage.sprite = sprite; 
    }

    public void Show(float fadeSpeed, int mode){
        StartCoroutine(Fade(fadeSpeed, mode));
    }

    public void Hide(float fadeSpeed, int mode){
        StartCoroutine(Fade(fadeSpeed, mode));
    }

    IEnumerator Fade(float fadeSpeed, int mode){
        print("callded");
        if(mode == 0){
            canvasGroup.alpha += 0.1f;
            yield return new WaitForSeconds(fadeSpeed);
        }else if(mode == 1 && canvasGroup.alpha > 0){
            canvasGroup.alpha -= 0.1f;
            yield return new WaitForSeconds(fadeSpeed);
        }
    }
}
