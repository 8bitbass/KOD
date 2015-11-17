using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManagerLogic : MonoBehaviour
{
    public float waveTime = 10f;
    private int lastWaveEnemyCount = 1;
    private static float currentWaveTime;

    public GameObject enemyTarget;

    private static int waveNumber = 1;
    public static int difficultyLevel = 5;

    public static List<EnemySpawner> spawners = new List<EnemySpawner>();
    public static List<EnemyBehavior> enemies = new List<EnemyBehavior>();

    // Use this for initialization
    void Start()
    {
        currentWaveTime = waveTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentWaveTime -= Time.deltaTime;
        if (currentWaveTime <= 0)
        {
            //EnemySpawner currentSpawner = GetClosest();
            //currentSpawner.SpawnEnemy(enemyTarget);
            currentWaveTime = waveTime;
            SpawnWave();
            waveNumber += 1;
        }
    }

    EnemySpawner GetClosest()
    {
        float distance = 9999999f;
        EnemySpawner currentClosest = null;
        foreach (EnemySpawner spawner in spawners)
        {
            float temp = (spawner.transform.position - enemyTarget.transform.position).magnitude;
            if (temp < distance)
            {
                distance = temp;
                currentClosest = spawner;
            }
        }
        return currentClosest;
    }

    private void SpawnWave()
    {
        int perdecAlive = (enemies.Count / lastWaveEnemyCount) * 10;
        int difficultyMod = perdecAlive * (difficultyLevel < 5 ? -1 : 1) + (waveNumber / 10) + difficultyLevel;

        Mathf.Clamp(difficultyMod, 1, 10);

        int enemiesToSpawn = difficultyLevel + difficultyMod;
        if(enemiesToSpawn < 4)
        {
            enemiesToSpawn = 4;
        }
        lastWaveEnemyCount = enemiesToSpawn;


        for (int i = 0; i <= enemiesToSpawn /4; ++i)
        {   
            //SpawnEnemies();
            Invoke("SpawnEnemies", i);
        }
    }

    void SpawnEnemies()
    {
        EnemySpawner currentSpawner = GetClosest();
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.SpawnEnemy(enemyTarget);
            }




            //int spawnerNum = i % 2;
            //if (spawnerNum == 1)
            //{
            //    currentSpawner.SpawnEnemy(enemyTarget);
            //}
            //else
            //{
            //    int currentNum = spawners.IndexOf(currentSpawner);
            //    int rando = Random.Range(0, spawners.Count - 1);

            //    spawners[rando].SpawnEnemy(enemyTarget);

            //}
    }
}
