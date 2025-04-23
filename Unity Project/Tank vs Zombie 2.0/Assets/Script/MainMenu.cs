using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This function is called when the Start Game button is clicked
    public void StartGame()
    {
        // Replace "Game" with the name of your gameplay scene
        SceneManager.LoadScene("Mode");
    }

    // This function is called when the Quit Game button is clicked
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // This shows in editor
        Application.Quit();     // This works in the final build
    }
}
