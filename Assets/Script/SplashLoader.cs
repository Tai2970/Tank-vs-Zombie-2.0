using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLoader : MonoBehaviour
{
    public float splashDuration = 2f;
    public string nextSceneName = "MainMenu"; // Set this in Inspector
    public AudioSource introSound; // Drag and assign your intro sound here

    void Start()
    {
        // Play the intro sound once at the beginning (if assigned)
        if (introSound != null)
        {
            introSound.Play();
        }

        // Wait splashDuration seconds, then load the next scene
        Invoke("LoadNextScene", splashDuration);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
