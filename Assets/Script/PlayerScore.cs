using UnityEngine;
using TMPro; 

public class PlayerScore : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI scoreText; 

    void Start()
    {
        score = 0;
        UpdateScoreUI();
    }

    public static void AddPoint()
    {
        score++;
        PlayerScore instance = FindAnyObjectByType<PlayerScore>();
        if (instance != null)
        {
            instance.UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}