using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour
{
    public static UIFader Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeIn(CanvasGroup canvasGroup, float duration, System.Action onComplete = null)
    {
        StartCoroutine(Fade(canvasGroup, 0f, 1f, duration, onComplete));
    }

    public void FadeOut(CanvasGroup canvasGroup, float duration, System.Action onComplete = null)
    {
        StartCoroutine(Fade(canvasGroup, 1f, 0f, duration, onComplete));
    }

    private System.Collections.IEnumerator Fade(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration, System.Action onComplete)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
        onComplete?.Invoke();
    }
}