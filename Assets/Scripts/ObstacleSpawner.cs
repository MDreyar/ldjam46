﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Tooltip("All spawnable obstacles")]
    public GameObject[] obstacles;

    private Thread SpawnObstacles;

    [Range(1f, 10f)]
    public float difficultyModifier;

    [Header("Balloon frequency"), Range(0f, 1f)]
    public float balloonFrequency;

    void Start()
    {
        GameManager.Instance.ObstacleSpawner = this;
        GameManager.Instance.OnGameStateChanged += GameStateChanged;
    }

    void SpawnObstacle(ObstacleHeight obsHeight)
    {
        int height;
        if (obsHeight == ObstacleHeight.LOW)
            height = 38;
        else if (obsHeight == ObstacleHeight.MID)
            height = 39;
        else if (obsHeight == ObstacleHeight.HIGH)
            height = 40;
        else
            height = 40;
        GameObject obstacle = Instantiate(obstacles[Random.Range(0f, (float)obstacles.Length) > balloonFrequency ? 0 : 1], this.transform);
        obstacle.transform.position = new Vector3(obstacle.transform.position.x + getXSpawnPos(), obstacle.transform.position.y + (-1) * height, obstacle.transform.position.z);
        obstacle.transform.rotation = Quaternion.Euler(180, 180, 0);
    }

    IEnumerator TimedSpawn()
    {
        while (GameManager.Instance.currentState == GameManager.GameState.playing)
        {
            SpawnObstacle((ObstacleHeight)Random.Range(0, 2));
            yield return new WaitForSeconds(1 / difficultyModifier);
        }
    }

    void GameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.playing)
        {
            StartCoroutine(TimedSpawn());
        }
    }

    private float getXSpawnPos()
    {
        float output = 0;
        while(-.5 < output && output < 0.5)
        {
            output = Random.Range(-4, 4);
        }
        return output;
    }

    public void Cleanup()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}

public enum ObstacleHeight
{
    HIGH,
    MID,
    LOW
}
