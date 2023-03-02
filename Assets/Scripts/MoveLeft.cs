using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 5;

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (!playerController.isGameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
    }
}
