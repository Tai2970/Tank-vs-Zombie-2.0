using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WaitSceneManager : MonoBehaviour
{
    [Header("Wait Scene Settings")]
    public TMP_Text waitText;
    public float dotSpeed = 0.5f;
    public float waitTime = 4f;

    private float dotTimer = 0f;
    private int dotCount = 0;
    private float sceneTimer = 0f;

    private string baseMessage = "Loading mission zone. Please stand by";
    private string nextSceneName;

    void Start()
    {
        // Load the next scene name from PlayerPrefs
        nextSceneName = PlayerPrefs.GetString("NextMap", "MainMenu");
    }

    void Update()
    {
        dotTimer += Time.deltaTime;
        if (dotTimer >= dotSpeed)
        {
            dotTimer = 0f;
            dotCount = (dotCount + 1) % 4;
            waitText.text = baseMessage + new string('.', dotCount);
        }

        sceneTimer += Time.deltaTime;
        if (sceneTimer >= waitTime)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
