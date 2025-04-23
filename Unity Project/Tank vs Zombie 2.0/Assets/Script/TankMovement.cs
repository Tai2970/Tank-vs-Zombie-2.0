using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public enum TankControlType { Player1, Player2, AI }
    public TankControlType controlType = TankControlType.Player1;

    public float moveSpeed = 8f;
    public float turnSpeed = 100f;

    private void Update()
    {
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
    }

    void HandlePlayer1()
    {
        float move = 0f;
        float turn = 0f;

        if (Input.GetKey(KeyCode.W)) move = 1f;
        else if (Input.GetKey(KeyCode.S)) move = -1f;

        if (Input.GetKey(KeyCode.A)) turn = -1f;
        else if (Input.GetKey(KeyCode.D)) turn = 1f;

        MoveTank(move, turn);
    }

    void HandlePlayer2()
    {
        float move = 0f;
        float turn = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) move = 1f;
        else if (Input.GetKey(KeyCode.DownArrow)) move = -1f;

        if (Input.GetKey(KeyCode.LeftArrow)) turn = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) turn = 1f;

        MoveTank(move, turn);
    }

    void HandleAI()
    {
        // Do nothing – server AI movement is handled by ServerAIController.cs
    }

    void MoveTank(float move, float turn)
    {
        transform.Translate(Vector3.forward * move * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * turn * turnSpeed * Time.deltaTime);
    }
}
