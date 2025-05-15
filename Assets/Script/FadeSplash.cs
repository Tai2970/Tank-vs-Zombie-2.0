using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeSplash : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 0.5f;
    public float logoDisplayTime = 4f;
    public string nextSceneName = "MainMenu";

    void Start()
    {
        StartCoroutine(PlaySplashSequence());
    }

    IEnumerator PlaySplashSequence()
    {
        // Fade from black to clear
        yield return StartCoroutine(Fade(1, 0));

        // Wait while logo is shown
        yield return new WaitForSeconds(logoDisplayTime);

        // Fade from clear to black
        yield return StartCoroutine(Fade(0, 1));

        // Load next scene
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = color;

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure final alpha
        color.a = endAlpha;
        fadeImage.color = color;
    }
}
