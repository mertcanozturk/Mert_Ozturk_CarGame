using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMovingSimulator : MonoBehaviour
{
    public List<DataCarMovement> data;
    public bool canMove = false;
    public float speed;
    public float rotationSpeed;

    void Start()
    {
        GameManager.OnChangeGameState += OnChangeGameState;
        StartCoroutine(StartSimulation());
    }

    void OnChangeGameState(StateTypes type)
    {
        if (type == StateTypes.Playing)
            canMove = true;
        else
            canMove = false;
    }

    IEnumerator StartSimulation()
    {
        yield return new WaitUntil(() => data != null && canMove);

        float startTime = Time.time;

        foreach (var item in data)
        {
            while (Time.time - startTime < item.startTime) yield return null;

            while (Time.time - startTime < item.endTime)
            {
                if (item.MovementType == DataCarMovement.MoveType.Left)
                    transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
                else
                    transform.Rotate(0, -Time.deltaTime * rotationSpeed, 0);
                yield return null;
            }

            yield return null;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.SetGameState(StateTypes.Failed);
        }
    }


    void Update()
    {
        if (canMove)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
    }
}
