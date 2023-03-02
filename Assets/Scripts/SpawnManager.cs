using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnObjects;
    
    private float lowerBound = -7f;
    private float upperBound = 5f;
    private float rightBound = 15f;

    private float timeToStart = 1;
    private float timeBetweenObjects = 2;

    private PlayerController playerController;
    

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        
        InvokeRepeating("SpawnRandomObject", timeToStart, timeBetweenObjects);
    }

    private Vector3 SpawnRandomPosition()
    {
        // We compute a random side
        float x = rightBound; // By default, we appear on the right side
        
        if (Random.Range(0, 2) == 0)
        {
            // If 0, we appear on the left side
            x *= -1;
        }
        
        // We compute a random height
        float y = Random.Range(lowerBound, upperBound);
        
        return new Vector3(x, y, 0);
    }

    private void SpawnRandomObject()
    {
        if (playerController.isGameOver)
        {
            CancelInvoke("SpawnRandomObject");
        }
        
        int randomIndex = Random.Range(0, spawnObjects.Length);

        GameObject obj = Instantiate(spawnObjects[randomIndex], SpawnRandomPosition(), Quaternion.identity);
        
        if (obj.transform.position.x < 0) // If the object appears on the left side
        {
            obj.GetComponent<MoveLeft>().speed *= -1; // We multiply its velocity by -1
        }
    }
}
