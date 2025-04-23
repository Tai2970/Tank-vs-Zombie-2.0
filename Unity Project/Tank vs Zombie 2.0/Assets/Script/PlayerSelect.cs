using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    public void StartOnePlayer()
    {
        Debug.Log("START 1 PLAYER CLICKED");
        PlayerPrefs.SetInt("PlayerCount", 1);
        SceneManager.LoadScene("TransitionScene");  
    }

    public void StartTwoPlayer()
    {
        Debug.Log("START 2 PLAYER CLICKED");
        PlayerPrefs.SetInt("PlayerCount", 2);
        SceneManager.LoadScene("TransitionScene");  
    }

    public void StartPlayerWithServer()
    {
        Debug.Log("START PLAYER + SERVER (AI) CLICKED");
        PlayerPrefs.SetInt("PlayerCount", 3);
        SceneManager.LoadScene("TransitionScene");  
    }
}
