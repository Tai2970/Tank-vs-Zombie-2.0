using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    public GameObject player1TankPrefab;
    public GameObject player2TankPrefab;
    public GameObject aiTankPrefab;

    public Transform spawnPoint1;
    public Transform spawnPoint2;

    void Start()
    {
        int playerCount = PlayerPrefs.GetInt("PlayerCount", 1);

        GameObject p1 = null;
        GameObject p2 = null;

        if (playerCount == 1)
        {
            // Single player mode: RED tank only
            p1 = Instantiate(player1TankPrefab, spawnPoint1.position, spawnPoint1.rotation);
            p1.tag = "Player";  // Ghosts will detect this one
        }
        else if (playerCount == 2)
        {
            // Two player mode: RED + BLUE
            p1 = Instantiate(player1TankPrefab, spawnPoint1.position, spawnPoint1.rotation);
            p2 = Instantiate(player2TankPrefab, spawnPoint2.position, spawnPoint2.rotation);

            p1.tag = "Player";   // Main player (ghost target)
            p2.tag = "Player2";  // Multiplayer player 2
        }
        else if (playerCount == 3)
        {
            // Server mode: RED + AI
            p1 = Instantiate(player1TankPrefab, spawnPoint1.position, spawnPoint1.rotation);
            p2 = Instantiate(aiTankPrefab, spawnPoint2.position, spawnPoint2.rotation);

            p1.tag = "Player"; // Main player (ghost target)
            p2.tag = "AI";     // Server-controlled tank
        }

        // Always assign camera to follow Player1
        MultiplayerCameraFollow camFollow = Camera.main.GetComponent<MultiplayerCameraFollow>();
        if (camFollow != null)
        {
            if (p1 != null) camFollow.AssignPlayer1(p1.transform);
            if (p2 != null && playerCount == 2)
                camFollow.AssignPlayer2(p2.transform);
        }
    }
}
