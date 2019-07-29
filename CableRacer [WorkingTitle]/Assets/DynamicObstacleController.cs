using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObstacleController : MonoBehaviour
{
    public ObstacleContainer _obstacleContainer;
    public List<GameObject> Obstacles = new List<GameObject>();
    public List<GameObject> ActiveObstacles = new List<GameObject>();
    public float SpeedDivider;
    
    public int CurrentPos;

    void Start()
    {
        CurrentPos = 0;
        
        foreach (var obstacle in Obstacles)
        {
            if (obstacle.active)
            {
                ActiveObstacles.Add(obstacle.gameObject);
            }
        }

        Obstacles = _obstacleContainer._obstacles;
        InvokeRepeating("ChangePos", 0.408f*SpeedDivider, 0.408f*SpeedDivider);
    }

    private void ChangePos()
    {
        Debug.Log("Changed Pos");

        foreach (var obstacle in Obstacles)
        {
            obstacle.SetActive(false);
        }

        if (CurrentPos < 5)
        {
            CurrentPos++;
        }
        else
        {
            CurrentPos = 0;
        }
        
        Obstacles[CurrentPos].gameObject.SetActive(true);
    }
}