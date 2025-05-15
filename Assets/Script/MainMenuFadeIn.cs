using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuFadeIn : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2.5f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            color.a = Mathf.Lerp(1, 0, t);
            fadeImage.color = color;

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure completely transparent
        color.a = 0;
        fadeImage.color = color;
    }
}
