using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryBoard : MonoBehaviour
{
    public GameObject victoryCanvas;
    public float delayBeforeTeleport = 3f;
    public string waitSceneName = "WaitScene"; // Used for transition after victory

    void Start()
    {
        // Hide victory UI when game starts
        if (victoryCanvas != null)
            victoryCanvas.SetActive(false);
    }

    public void ShowVictory()
    {
        // Show the victory canvas
        if (victoryCanvas != null)
            victoryCanvas.SetActive(true);

        // Play win sound using the GameResultSoundManager on VictoryManager object
        GameResultSoundManager soundManager = GameObject.Find("VictoryManager")?.GetComponent<GameResultSoundManager>();
        if (soundManager != null)
        {
            soundManager.PlayResultSound();
        }

        // Pause game time while the board is showing
        Time.timeScale = 0f;

        // Begin delayed transition
        StartCoroutine(WaitThenTeleport());
    }

    System.Collections.IEnumerator WaitThenTeleport()
    {
        float counter = 0f;
        while (counter < delayBeforeTeleport)
        {
            counter += Time.unscaledDeltaTime;
            yield return null;
        }

        // Resume game time
        Time.timeScale = 1f;

        // Determine next map
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "GhostValley")
        {
            PlayerPrefs.SetString("NextMap", "CrimsonArsenal");
        }
        else if (currentScene == "CrimsonArsenal")
        {
            PlayerPrefs.SetString("NextMap", "SteelUndead");
        }
        else if (currentScene == "SteelUndead")
        {
            PlayerPrefs.SetString("NextMap", "Maintenance");
        }

        // Load the WaitScene
        SceneManager.LoadScene(waitSceneName);
    }
}
