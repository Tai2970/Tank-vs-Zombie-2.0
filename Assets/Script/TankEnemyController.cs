using UnityEngine;
using UnityEngine.SceneManagement;

public class TankEnemyController : MonoBehaviour
{
    [Header("Enemy Type")]
    public bool isSpecialEnemy = false;

    [Header("Floating Text Settings")]
    [SerializeField] private GameObject plusOnePrefab;

    private GameObject floatingTextRoot;
    private GameObject gameOverCanvas;
    private bool hasBeenHit = false;

    void Start()
    {
        // Play spawn sound when enemy appears
        EnemySoundManager soundManager = GetComponent<EnemySoundManager>();
        if (soundManager != null)
        {
            soundManager.PlaySpawnSound();
        }

        floatingTextRoot = GameObject.FindGameObjectWithTag("FloatingTextRoot");

        // Only regular (non-special) tanks trigger Game Over
        if (!isSpecialEnemy)
        {
            foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (obj.name == "GameOverCanvas")
                {
                    gameOverCanvas = obj;
                    gameOverCanvas.SetActive(false);
                    break;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasBeenHit) return;

        // Bullet hits all enemy types
        if (other.CompareTag("Bullet"))
        {
            hasBeenHit = true;
            PlayerScore.AddPoint();
            SpawnPlusOneText();
            Destroy(gameObject);
        }

        // Only regular tanks kill players and trigger Game Over
        if (!isSpecialEnemy)
        {
            int mode = PlayerPrefs.GetInt("PlayerCount", 1);

            if (other.CompareTag("Player"))
            {
                other.gameObject.SetActive(false);

                bool isPlayer2Alive = GameObject.FindWithTag("Player2")?.activeInHierarchy == true;

                // Game Over if: solo mode, AI mode, or co-op with Player 2 already dead
                if (mode == 1 || mode == 3 || (mode == 2 && !isPlayer2Alive))
                {
                    TriggerGameOver();
                }
            }
            else if (other.CompareTag("Player2"))
            {
                other.gameObject.SetActive(false);

                bool isPlayer1Alive = GameObject.FindWithTag("Player")?.activeInHierarchy == true;

                // Game Over only if Player 1 is also dead in co-op mode
                if (mode == 2 && !isPlayer1Alive)
                {
                    TriggerGameOver();
                }
            }
        }
    }

    void TriggerGameOver()
    {
        SurvivalManager sm = Object.FindFirstObjectByType<SurvivalManager>();
        if (sm != null)
            sm.OnPlayerDeath();

        if (gameOverCanvas != null)
            gameOverCanvas.SetActive(true);
    }

    void SpawnPlusOneText()
    {
        if (plusOnePrefab != null && floatingTextRoot != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            GameObject plusOne = Instantiate(plusOnePrefab, floatingTextRoot.transform);
            plusOne.transform.position = screenPos;
        }
    }
}
