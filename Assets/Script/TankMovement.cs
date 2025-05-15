using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public enum TankControlType { Player1, Player2, AI }
    public TankControlType controlType = TankControlType.Player1;

    public float moveSpeed = 12f;
    public float turnSpeed = 100f;
    public float acceleration = 5f;
    public float deceleration = 5f;

    public bool isInputLocked = false;

    private float currentMoveInput = 0f;
    private float currentTurnInput = 0f;

    private TankSoundManager soundManager;

    void Start()
    {
        soundManager = GetComponent<TankSoundManager>();
    }

    void Update()
    {
        if (isInputLocked) return;

        switch (controlType)
        {
            case TankControlType.Player1:
                HandlePlayer1();
                break;
            case TankControlType.Player2:
                HandlePlayer2();
                break;
            case TankControlType.AI:
                HandleAI();
                break;
        }

        MoveTank();
    }

    void HandlePlayer1()
    {
        float targetMove = 0f;
        float targetTurn = 0f;

        if (Input.GetKey(KeyCode.W)) targetMove = 1f;
        else if (Input.GetKey(KeyCode.S)) targetMove = -1f;

        if (Input.GetKey(KeyCode.A)) targetTurn = -1f;
        else if (Input.GetKey(KeyCode.D)) targetTurn = 1f;

        currentMoveInput = Mathf.Lerp(currentMoveInput, targetMove, acceleration * Time.deltaTime);
        currentTurnInput = Mathf.Lerp(currentTurnInput, targetTurn, acceleration * Time.deltaTime);

        if (Mathf.Abs(targetMove) > 0 && soundManager != null)
        {
            soundManager.PlayMoveSound();
        }
    }

    void HandlePlayer2()
    {
        float targetMove = 0f;
        float targetTurn = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) targetMove = 1f;
        else if (Input.GetKey(KeyCode.DownArrow)) targetMove = -1f;

        if (Input.GetKey(KeyCode.LeftArrow)) targetTurn = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) targetTurn = 1f;

        currentMoveInput = Mathf.Lerp(currentMoveInput, targetMove, acceleration * Time.deltaTime);
        currentTurnInput = Mathf.Lerp(currentTurnInput, targetTurn, acceleration * Time.deltaTime);

        if (Mathf.Abs(targetMove) > 0 && soundManager != null)
        {
            soundManager.PlayMoveSound();
        }
    }

    void HandleAI()
    {
        // ServerAIController handles this.
    }

    void MoveTank()
    {
        transform.Translate(Vector3.forward * currentMoveInput * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * currentTurnInput * turnSpeed * Time.deltaTime);
    }
}
