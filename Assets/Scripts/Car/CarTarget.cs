using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTarget : MonoBehaviour
{
    public GameObject myCar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == myCar)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (GameManager.Instance.GameState != StateTypes.LevelUp)
                    GameManager.Instance.SetGameState(StateTypes.LevelUp);
            }
            else if (other.TryGetComponent(out CarMovingSimulator carMovingSimulator))
            {
                carMovingSimulator.canMove = false;
                carMovingSimulator.StopAllCoroutines();
            }

        }
    }
}
