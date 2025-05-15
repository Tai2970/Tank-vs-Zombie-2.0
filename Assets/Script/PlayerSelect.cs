using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    // Reference to the AudioSource that plays button click sounds
    public AudioSource clickSound;

    // Called when "One Player" button is clicked
    public void StartOnePlayer()
    {
        PlayClick();
        Invoke("LoadOnePlayer", 0.3f);
    }

    // Called when "Two Players" button is clicked
    public void StartTwoPlayer()
    {
        PlayClick();
        Invoke("LoadTwoPlayer", 0.3f);
    }

    // Called when "Server" button is clicked
    public void StartPlayerWithServer()
    {
        PlayClick();
        Invoke("LoadServerPlayer", 0.3f);
    }

    // Loads the next scene for one player mode
    void LoadOnePlayer()
    {
        Debug.Log("START 1 PLAYER CLICKED");
        PlayerPrefs.SetInt("PlayerCount", 1);
        PlayerPrefs.SetString("NextMap", "GhostValley");
        SceneManager.LoadScene("WaitScene");
    }

    // Loads the next scene for two players
    void LoadTwoPlayer()
    {
        Debug.Log("START 2 PLAYER CLICKED");
        PlayerPrefs.SetInt("PlayerCount", 2);
        PlayerPrefs.SetString("NextMap", "GhostValley");
        SceneManager.LoadScene("WaitScene");
    }

    // Loads the next scene with server-controlled AI
    void LoadServerPlayer()
    {
        Debug.Log("START PLAYER + SERVER (AI) CLICKED");
        PlayerPrefs.SetInt("PlayerCount", 3);
        PlayerPrefs.SetString("NextMap", "GhostValley");
        SceneManager.LoadScene("WaitScene");
    }

    // Plays the click sound if an AudioSource is assigned
    void PlayClick()
    {
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }
}
