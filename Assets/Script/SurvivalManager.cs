using UnityEngine;
using UnityEngine.UI;

public class SurvivalManager : MonoBehaviour
{
    public enum SpawnerType { AutoDetect, Ghost, Tank, None }

    [Header("Spawner Settings")]
    public SpawnerType spawnerType = SpawnerType.AutoDetect;

    [Header("UI Settings")]
    public Text timerText;

    private float survivalTime = 300f; // ⏱ Fixed 5 minutes
    private bool timeUp = false;
    private bool playerDead = false;

    private MonoBehaviour enemySpawner;
    private GameObject gameOverCanvas;
    private VictoryBoard victoryBoard;

    void Start()
    {
        FindEnemySpawner();
        FindGameOverCanvas();
        victoryBoard = Object.FindFirstObjectByType<VictoryBoard>();
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (playerDead || timeUp || Time.timeScale == 0f) return;

        survivalTime -= Time.deltaTime;
        UpdateTimerDisplay();

        if (survivalTime <= 0f)
        {
            survivalTime = 0f;
            UpdateTimerDisplay();
            HandleVictory();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(survivalTime / 60f);
        int seconds = Mathf.FloorToInt(survivalTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void HandleVictory()
    {
        StopEnemySpawner();
        DestroyAllEnemies();
        LockAllTankInputs();
        timeUp = true;

        if (victoryBoard != null)
            victoryBoard.ShowVictory();
        else
            Debug.LogError("VictoryBoard not found in scene.");
    }

    public void OnPlayerDeath()
    {
        bool player1Alive = GameObject.FindWithTag("Player")?.activeInHierarchy == true;
        bool player2Alive = GameObject.FindWithTag("Player2")?.activeInHierarchy == true;

        if (player1Alive || player2Alive)
            return;

        playerDead = true;
        timeUp = true;

        StopEnemySpawner();
        DestroyAllEnemies();
        LockAllTankInputs();

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
            GameResultSoundManager soundManager = gameOverCanvas.GetComponent<GameResultSoundManager>();
            if (soundManager != null)
                soundManager.PlayResultSound();
        }
        else
        {
            Debug.LogWarning("GameOverCanvas is null. Cannot show Game Over screen.");
        }
    }

    void FindEnemySpawner()
    {
        switch (spawnerType)
        {
            case SpawnerType.Ghost:
                enemySpawner = Object.FindFirstObjectByType<GhostSpawner>();
                break;
            case SpawnerType.Tank:
                enemySpawner = Object.FindFirstObjectByType<TankEnemySpawner>();
                break;
            case SpawnerType.AutoDetect:
                enemySpawner = Object.FindFirstObjectByType<GhostSpawner>();
                if (enemySpawner == null)
                    enemySpawner = Object.FindFirstObjectByType<TankEnemySpawner>();
                break;
            case SpawnerType.None:
                enemySpawner = null;
                break;
        }
    }

    void StopEnemySpawner()
    {
        if (enemySpawner != null)
        {
            enemySpawner.enabled = false;
            enemySpawner.StopAllCoroutines();
        }
    }

    void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    void LockAllTankInputs()
    {
        foreach (TankMovement tm in Object.FindObjectsByType<TankMovement>(FindObjectsSortMode.None))
        {
            tm.isInputLocked = true;
        }

        foreach (TankShooter ts in Object.FindObjectsByType<TankShooter>(FindObjectsSortMode.None))
        {
            ts.isInputLocked = true;
        }
    }

    void FindGameOverCanvas()
    {
        Transform canvasRoot = GameObject.Find("Canvas")?.transform;
        if (canvasRoot != null)
        {
            Transform child = canvasRoot.Find("GameOverCanvas");
            if (child != null)
            {
                gameOverCanvas = child.gameObject;
                gameOverCanvas.SetActive(false);
                return;
            }
        }

        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "GameOverCanvas" && obj.hideFlags == HideFlags.None)
            {
                gameOverCanvas = obj;
                gameOverCanvas.SetActive(false);
                return;
            }
        }

        Debug.LogWarning("GameOverCanvas not found. Please check scene setup.");
    }

    public float GetRemainingTime()
    {
        return survivalTime;
    }
}
