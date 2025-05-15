using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource clickSound;

    // Called when the "Play" button is clicked
    public void StartGame()
    {
        PlayClick();
        Invoke("LoadGameScene", 0.3f); // Delay loading scene to let sound play
    }

    // Called when the "Quit" button is clicked
    public void QuitGame()
    {
        PlayClick();
        Debug.Log("Quit Game");
        Application.Quit();
    }

    // Called in MaintenanceScene to return to the main menu
    public void ReturnToMainMenu()
    {
        PlayClick();
        SceneManager.LoadScene("MainMenu");
    }

    // Loads the Mode scene after the sound delay
    void LoadGameScene()
    {
        SceneManager.LoadScene("Mode");
    }

    // Plays the click sound if an AudioSource is assigned
    void PlayClick()
    {
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }

    // Optional: This method can be linked to UI elements directly for sound-only clicks
    public void PlayClickManually()
    {
        PlayClick();
    }
}
