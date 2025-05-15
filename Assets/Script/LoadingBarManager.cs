using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBarManager : MonoBehaviour
{
    public Slider loadingBar;
    public float delayBeforeStart = 0f; // Wait few seconds before starting
    public float loadTime = 0f;         // Time to fill bar after delay

    private float timer = 0f;
    private bool loadingStarted = false;

    void Update()
    {
        // Wait for the delay first
        if (!loadingStarted)
        {
            timer += Time.deltaTime;
            if (timer >= delayBeforeStart)
            {
                loadingStarted = true;
                timer = 0f; // reset timer to start filling bar
            }
            return;
        }

        // Fill the loading bar
        if (timer < loadTime)
        {
            timer += Time.deltaTime;
            loadingBar.value = timer / loadTime;
        }
        else
        {
            SceneManager.LoadScene("GhostValley");
        }
    }
}
