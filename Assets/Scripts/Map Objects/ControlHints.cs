using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControlHints : MonoBehaviour
{
    [SerializeField] Sprite sprite;
    private Image hintImage;
    private CanvasGroup canvasGroup;
    ChestBehaviour parent;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        hintImage = GetComponent<Image>();
        hintImage.sprite = sprite;
        parent = GetComponentInParent<ChestBehaviour>();
    }

    public void Show(float fadeSpeed)
    {
        StartCoroutine(Fade(1f, fadeSpeed));
    }

    public void Hide(float fadeSpeed)
    {
        StartCoroutine(Fade(0f, fadeSpeed));
    }

    IEnumerator Fade(float targetAlpha, float fadeSpeed)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (Mathf.Abs(canvasGroup.alpha - targetAlpha) > 0.01f)
        {
            time += Time.deltaTime / fadeSpeed;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        if(parent.isOpen){
            this.enabled = false;
        }
    }
}
