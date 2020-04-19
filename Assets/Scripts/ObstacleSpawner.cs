using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Tooltip("All spawnable obstacles")]
    public GameObject[] obstacles;

    void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        GameObject obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Length)], this.transform);
        obstacle.transform.position = new Vector3(obstacle.transform.position.x + Random.Range(-4, 4), obstacle.transform.position.y + (-1)*(Random.Range(37, 40)), obstacle.transform.position.z);
        obstacle.transform.rotation = Quaternion.Euler(180, 180, 0);
    }
    
    void Update()
    {
        
    }
}
