using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EnemyManagerLogic : MonoBehaviour
{
    public static float waveTime = 7f;
    private static float currentWaveTime;

    public GameObject EnemyTarget;

    public static List<EnemySpawner> spawners = new List<EnemySpawner>();
    private List<EnemyBehavior> enemies;

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
            EnemySpawner currentSpawner = GetClosest();
            currentSpawner.SpawnEnemy(EnemyTarget);
            currentWaveTime = waveTime;
        }
    }

    EnemySpawner GetClosest()
    {
        float distance = 9999999f;
        EnemySpawner currentClosest = null;
        foreach (EnemySpawner spawner in spawners)
        {
            float temp = (spawner.transform.position - EnemyTarget.transform.position).magnitude;
            if (temp < distance)
            {
                distance = temp;
                currentClosest = spawner;
            }
        }
        return currentClosest;
    }

    void SpawnEnemies(int number)
    {
        EnemySpawner currentSpawner = GetClosest();
        for (int i = 0; i < number; ++i)
        {
            int spawnerNum = i % 2;
            if (spawnerNum == 1)
            {
                currentSpawner.SpawnEnemy(EnemyTarget);
            }
            else
            {
                int currentNum = spawners.IndexOf(currentSpawner);
                int rando = Random.Range(0, spawners.Count - 1);

                spawners[rando].SpawnEnemy(EnemyTarget);

            }
        }
    }
}
