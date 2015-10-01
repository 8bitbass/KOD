﻿using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour
{
    //member variables
    public float spawnTimer;
    public GameObject enemy;
    private float currentSpawnTime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentSpawnTime += Time.deltaTime;
        if(currentSpawnTime > spawnTimer)
        {
            GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity) as GameObject;
            currentSpawnTime = 0;
        }
    }
}