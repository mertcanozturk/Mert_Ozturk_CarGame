using cakeslice;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private PlayerController _playerController;

    [Header("Prefabs")]
    public GameObject carPrefab;
    public GameObject targetPrefab;
    public GameObject playerTargetPrefab;

    [Header("Runtime Variables")]

    public int currentLevel = 1;
    public int carQueue = 1;

    public List<LevelData> levels;
    public List<GameObject> sceneObjects;
    public List<GameObject> spawnedCars;

    private List<CarPathData> pathData;

    void Start()
    {
        GameManager.OnChangeGameState += OnChangeGameState;
        pathData = new List<CarPathData>();
        CreateTheScene();
    }

    void OnChangeGameState(StateTypes type)
    {
        switch (type)
        {
            case StateTypes.WaitingForPlay:
                break;
            case StateTypes.Playing:
                _playerController.StartPlayer();
                break;
            case StateTypes.LevelUp:
                _playerController.LevelUp();
                LevelUp();
                break;
            case StateTypes.Failed:
                ResetLevel();
                CreateTheScene();
                _playerController.PlayerFailed();
                break;
            default:
                break;
        }
    }

    void LevelUp()
    {
        if (carQueue == 8)
        {
            //Reset for new Level

            pathData.Clear();
            pathData = new List<CarPathData>();
            carQueue = 1;
            currentLevel++;
        }
        else
        {
            //Reset for new car 
            carQueue++;
            List<DataCarMovement> path = new List<DataCarMovement>(_playerController.movingData);
            CarPathData newData = new CarPathData();
            newData.carMovement = path;
            pathData.Add(newData);

        }

        _playerController.canMove = false;
        _playerController.movingData.Clear();
        _playerController.movementIndex = 0;

        //Create again the scene
        CreateTheScene();
    }

    void CreateTheScene()
    {
        //Destroy scene obstacles for new level.
        DestroyAllSceneObjects();

        //create new List
        sceneObjects = new List<GameObject>();

        //Create the obstacles.
        foreach (var obstacle in levels[currentLevel - 1].Obstacles)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>(obstacle.prefabPath), obstacle.position, Quaternion.Euler(obstacle.rotation));
            sceneObjects.Add(obj);
        }

        // Create simulated cars and its target.
        for (int i = 0; i < carQueue - 1; i++)
        {
            // Create simulated car.
            GameObject car = Instantiate(carPrefab, levels[currentLevel - 1].entrancePoints[i], Quaternion.identity);
            car.gameObject.tag = "SimulatedCar";
            car.GetComponentInChildren<Outline>().enabled = false;

            // Set simulated car properties.
            car.AddComponent<CarMovingSimulator>();
            CarMovingSimulator carMovingSimulator = car.GetComponent<CarMovingSimulator>();
            carMovingSimulator.speed = _playerController.speed;
            carMovingSimulator.rotationSpeed = _playerController.rotationSpeed;
            carMovingSimulator.data = pathData[i].carMovement;
            sceneObjects.Add(car);

            //Create car target.
            GameObject carTarget = Instantiate(targetPrefab, levels[currentLevel - 1].targetPoints[i], Quaternion.identity);
            carTarget.GetComponent<CarTarget>().myCar = car;
            sceneObjects.Add(carTarget);
        }


        //Create player car.
        GameObject playerCar = Instantiate(carPrefab, levels[currentLevel - 1].entrancePoints[carQueue - 1], Quaternion.identity);
        playerCar.gameObject.tag = "Player";
        _playerController.myCar = playerCar.transform;

        sceneObjects.Add(playerCar);

        //Create player target.
        GameObject target = Instantiate(playerTargetPrefab, levels[currentLevel - 1].targetPoints[carQueue - 1], Quaternion.identity);
        target.GetComponent<CarTarget>().myCar = playerCar;

        sceneObjects.Add(target);
    }

    void DestroyAllSceneObjects()
    {
        if (sceneObjects != null && sceneObjects.Count > 0)
        {
            for (int i = 0; i < sceneObjects.Count; i++)
            {
                Destroy(sceneObjects[i]);
            }
        }
    }

    void ResetLevel()
    {
        CreateTheScene();
        _playerController.movingData.Clear();
        _playerController.movementIndex = 0;
    }

}

