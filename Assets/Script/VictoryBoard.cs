using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryBoard : MonoBehaviour
{
    public GameObject victoryCanvas;
    public float delayBeforeTeleport = 3f;
    public string waitSceneName = "WaitScene"; // Always load WaitScene after victory

    void Start()
    {
        if (victoryCanvas != null)
            victoryCanvas.SetActive(false);
    }

    public void ShowVictory()
    {
        // Show the victory UI
        if (victoryCanvas != null)
            victoryCanvas.SetActive(true);

        // Play win sound via VictoryManager
        GameResultSoundManager soundManager = GameObject.Find("VictoryManager")?.GetComponent<GameResultSoundManager>();
        if (soundManager != null)
        {
            soundManager.PlayResultSound();
        }

        // Pause game while showing victory board
        Time.timeScale = 0f;
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

        // Decide which map to load next
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
            PlayerPrefs.SetString("NextMap", "Maintenance"); // Final floor leads to Maintenance
        }

        // Load WaitScene for transition
        SceneManager.LoadScene(waitSceneName);
    }
}
