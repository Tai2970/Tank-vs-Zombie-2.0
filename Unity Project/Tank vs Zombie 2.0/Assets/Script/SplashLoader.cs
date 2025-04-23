using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLoader : MonoBehaviour
{
    public float splashDuration = 2f; // Time in seconds

    void Start()
    {
        Invoke("LoadNextScene", splashDuration);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
