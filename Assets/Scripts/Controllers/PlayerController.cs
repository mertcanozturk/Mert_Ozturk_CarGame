using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player properties")]
    public bool canMove;
    public float speed = 5f;
    public float rotationSpeed = 2f;

    [Header("Runtime variables")]
    public Transform myCar;
    public List<DataCarMovement> movingData;
    public int movementIndex = 0;

    private float firstStartTime = 0;
    private bool isRecording;

    void Start()
    {
        movingData = new List<DataCarMovement>();
        canMove = false;
        isRecording = false;
    }

    public void StartPlayer()
    {
        canMove = true;
        firstStartTime = Time.time;
    }

    public void LevelUp()
    {
        if (isRecording) movingData[movementIndex].endTime = Time.time - firstStartTime;
        isRecording = false;
        canMove = false;
    }

    public void PlayerFailed()
    {
        isRecording = false;
        canMove = false;
        movingData.Clear();
        movementIndex = 0;

        GameManager.Instance.GameState = StateTypes.WaitingForPlay;

    }

    void Update()
    {
        MoveCar();

        GetInputToStartGame();

        if (!canMove) return;

        GetCarDrivingInput();
    }

    void MoveCar()
    {
        if (canMove)
        {
            myCar.transform.position += myCar.transform.forward * Time.deltaTime * speed;
        }
    }

    void GetInputToStartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.Instance.GameState != StateTypes.Playing)
            {
                GameManager.Instance.SetGameState(StateTypes.Playing);

                return;
            }
            else return;
        }
    }

    void GetCarDrivingInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > (Screen.width / 2))
            {
                if (!isRecording)
                {
                    isRecording = true;
                    DataCarMovement data = new DataCarMovement();
                    data.MovementType = DataCarMovement.MoveType.Left;
                    data.startTime = Time.time - firstStartTime;
                    movingData.Add(data);

                }

                myCar.transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
            }
            else
            {
                if (!isRecording)
                {
                    isRecording = true;
                    DataCarMovement data = new DataCarMovement();
                    data.MovementType = DataCarMovement.MoveType.Right;
                    data.startTime = Time.time - firstStartTime;

                    movingData.Add(data);

                }

                myCar.transform.Rotate(0, -Time.deltaTime * rotationSpeed, 0);
            }
        }
        else
        {
            if (isRecording)
            {
                movingData[movementIndex].endTime = Time.time - firstStartTime;
                isRecording = false;
                Debug.Log(movementIndex + ". movement saved");
                movementIndex++;
            }
        }
    }
}
