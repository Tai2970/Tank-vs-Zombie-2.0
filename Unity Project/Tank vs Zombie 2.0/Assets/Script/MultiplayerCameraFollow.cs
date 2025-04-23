using UnityEngine;

public class MultiplayerCameraFollow : MonoBehaviour
{
    private Transform player1;
    private Transform player2;
    private Camera cam;

    public Vector3 offset = new Vector3(0, 10, -10);
    public float followSpeed = 5f;

    public float minZoom = 40f;
    public float maxZoom = 70f;
    public float zoomLimiter = 10f;

    private int playerCount;

    void Start()
    {
        cam = Camera.main;
        playerCount = PlayerPrefs.GetInt("PlayerCount", 1);
    }

    void LateUpdate()
    {
        if (player1 == null)
            return;

        if (playerCount == 1 || playerCount == 3)
        {
            // Single or Server mode: Follow Player1 only
            Vector3 targetPos = player1.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
            transform.LookAt(player1);
        }
        else if (playerCount == 2 && player1 != null && player2 != null)
        {
            // Two Player mode: Follow midpoint
            Vector3 centerPoint = GetCenterPoint();
            Vector3 newPos = centerPoint + offset;
            transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
            transform.LookAt(centerPoint);

            float distance = Vector3.Distance(player1.position, player2.position);
            float newZoom = Mathf.Lerp(maxZoom, minZoom, distance / zoomLimiter);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
        }
    }

    Vector3 GetCenterPoint()
    {
        return (player1.position + player2.position) / 2f;
    }

    // These will be called from TankSpawner
    public void AssignPlayer1(Transform t) => player1 = t;
    public void AssignPlayer2(Transform t) => player2 = t;
}
