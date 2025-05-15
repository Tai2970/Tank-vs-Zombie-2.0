using UnityEngine;
using UnityEngine.UI;

public class IntroBoard : MonoBehaviour
{
    public GameObject introCanvas;       // Canvas shown at the beginning with instructions
    public Button okButton;              // OK button to continue to gameplay

    public GameObject gameTimer;         // Timer UI to activate after intro
    public GameObject scoreDisplay;      // Score UI to activate after intro

    [Header("Audio")]
    public AudioClip introSound;

    private AudioSource audioSource;

    void Start()
    {
        // Setup audio source
        audioSource = GetComponent<AudioSource>();
        if (introSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(introSound);
        }

        // Show intro canvas and hide gameplay UI
        introCanvas.SetActive(true);
        gameTimer.SetActive(false);
        scoreDisplay.SetActive(false);

        // Freeze gameplay
        Time.timeScale = 0f;

        // Hook up OK button
        okButton.onClick.AddListener(OnOKClick);
    }

    public void OnOKClick()
    {
        // Hide intro canvas and show game UI
        introCanvas.SetActive(false);
        gameTimer.SetActive(true);
        scoreDisplay.SetActive(true);

        // Resume gameplay
        Time.timeScale = 1f;
    }

    public bool IsIntroShowing()
    {
        return introCanvas != null && introCanvas.activeSelf;
    }
}
