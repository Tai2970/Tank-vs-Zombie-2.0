using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRadius = 10f;

    private Vector3 moveDirection;
    private Camera mainCam;
    private Renderer ghostRenderer;
    private Transform player;
    private GameObject gameOverCanvas;
    private GameObject floatingTextRoot;
    private bool hasBeenHit = false;

    [Header("Floating Text Settings")]
    [SerializeField] private GameObject plusOnePrefab;

    private GhostSoundManager soundManager;

    void Start()
    {
        mainCam = Camera.main;
        ghostRenderer = GetComponent<Renderer>();
        soundManager = GetComponent<GhostSoundManager>();

        // Play ghost sound on spawn
        if (soundManager != null)
        {
            soundManager.PlayGhostSound();
        }

        // Set initial random move direction
        Vector2 dir2D = Random.insideUnitCircle.normalized;
        moveDirection = new Vector3(dir2D.x, 0f, dir2D.y);

        // Find GameOverCanvas in scene
        foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (obj.name == "GameOverCanvas")
            {
                gameOverCanvas = obj;
                gameOverCanvas.SetActive(false);
                break;
            }
        }

        floatingTextRoot = GameObject.FindGameObjectWithTag("FloatingTextRoot");
    }

    void Update()
    {
        UpdateClosestPlayer();

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer < detectionRadius)
            {
                Vector3 directionToPlayer = (player.position - transform.position).normalized;
                transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            }
        }
        else
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        }

        if (!IsVisible())
        {
            gameObject.SetActive(false);
        }
    }

    void UpdateClosestPlayer()
    {
        GameObject[] targets = new GameObject[]
        {
            GameObject.FindGameObjectWithTag("Player"),
            GameObject.FindGameObjectWithTag("Player2"),
            GameObject.FindGameObjectWithTag("AI")
        };

        float minDistance = Mathf.Infinity;
        player = null;

        foreach (GameObject target in targets)
        {
            if (target != null && target.activeInHierarchy)
            {
                float dist = Vector3.Distance(transform.position, target.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    player = target.transform;
                }
            }
        }
    }

    bool IsVisible()
    {
        return ghostRenderer != null && ghostRenderer.isVisible;
    }

    private void OnTriggerEnter(Collider other)
    {
        int mode = PlayerPrefs.GetInt("PlayerCount", 1);

        // Player 1 gets hit
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
        // Player 2 gets hit
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
        // Bullet hits ghost
        else if (other.CompareTag("Bullet") && !hasBeenHit)
        {
            hasBeenHit = true;
            PlayerScore.AddPoint();
            SpawnPlusOneText();
            Destroy(gameObject);
        }
    }

    void TriggerGameOver()
    {
        SurvivalManager sm = Object.FindFirstObjectByType<SurvivalManager>();
        if (sm != null)
        {
            sm.OnPlayerDeath();
        }

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    private void SpawnPlusOneText()
    {
        if (plusOnePrefab != null && floatingTextRoot != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            GameObject plusOne = Instantiate(plusOnePrefab, floatingTextRoot.transform);
            plusOne.transform.position = screenPosition;
        }
    }
}
