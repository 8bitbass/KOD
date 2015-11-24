using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManagerLogic : MonoBehaviour
{
    public float enemyKillTime = 5f;
    private int lastWaveEnemyCount = 1;
    public static float currentWaveTime;
    private bool waveOver = false;

    public GameObject enemyTarget;

    public static int waveNumber = 0;
    public static int difficultyLevel = 3;

    public static List<EnemySpawner> spawners = new List<EnemySpawner>();
    public static List<EnemyBehavior> enemies = new List<EnemyBehavior>();
    public static List<EnemyBehavior> deadEnemies = new List<EnemyBehavior>();

    // Use this for initialization
    void Start()
    {
        currentWaveTime = enemyKillTime;
        InvokeRepeating("SetWaveState", 6, 1);
        Invoke("OrderSpawners", 2);
    }

    void OrderSpawners()
    {
        int numberOfSpawners = spawners.Count;
        EnemySpawner middle = GetFurthestSpawner(spawners[0]);
        spawners.Remove(middle);
        spawners.Insert((numberOfSpawners / 2), middle);
    }

    // Update is called once per frame
    void Update()
    {
        currentWaveTime -= Time.deltaTime;
        if (currentWaveTime <= 0 || waveOver)
        {
            waveOver = false;
            waveNumber += 1;
            SpawnWave();
            currentWaveTime = (enemyKillTime * (4 * ((waveNumber + 9) / 10) + waveNumber)) - (difficultyLevel * (waveNumber /10));
        } 
    }

    void LateUpdate()
    {
        DeleteDeadEnemies();
    }

    static void DeleteDeadEnemies()
    {
        foreach (EnemyBehavior enemy in deadEnemies)
        {
            GameManager.score += enemy.points;
            enemies.Remove(enemy);
            GameObject.Destroy(enemy.gameObject);
        }
        deadEnemies.Clear();
    }

    EnemySpawner GetClosestSpawnerToTarget()
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

    EnemySpawner GetFurthestSpawner(EnemySpawner spawner)
    {
        float distance = 0f;
        EnemySpawner opposite = null;
        foreach (EnemySpawner s in spawners)
        {
            float temp = (s.transform.position - spawner.transform.position).magnitude;
            if (temp > distance)
            {
                distance = temp;
                opposite = s;
            }
        }
        return opposite;
    }

    private void SpawnWave()
    {
        int enemiesRemaining = enemies.Count;
        int currentWaveEnemyCount = ((difficultyLevel + 1) * ((waveNumber + 9) / 10)) + waveNumber;

        switch (difficultyLevel)
        {
            case 1:
                currentWaveEnemyCount = lastWaveEnemyCount - enemiesRemaining;
                break;
            case 2:
                currentWaveEnemyCount -= enemiesRemaining;
                break;
            case 3:
                break;
            case 4:
                currentWaveEnemyCount += enemiesRemaining;
                break;
            case 5:
                currentWaveEnemyCount += enemiesRemaining * 2;
                break;
        }

        lastWaveEnemyCount = currentWaveEnemyCount + enemiesRemaining;
        SpawnEnemies(currentWaveEnemyCount);
    }

    void SpawnEnemies(int numberOfEnemies)
    {
        if (numberOfEnemies % 4 == 0)
        {
            for (int i = 0; i < numberOfEnemies / 4; ++i)
            {
                foreach (EnemySpawner spawner in spawners)
                {
                    StartCoroutine(Spawn(i, spawner));
                }
            }

        }
        else if (numberOfEnemies % 2 == 0)
        {
            for (int i = 0; i < numberOfEnemies / 2; ++i)
            {
                if (i % 2 == 0)
                {
                    StartCoroutine(Spawn(i, spawners[0]));
                    StartCoroutine(Spawn(i, spawners[spawners.Count/2]));
                }
                else
                {
                    StartCoroutine(Spawn(i, spawners[1]));
                    StartCoroutine(Spawn(i, spawners[spawners.Count -1]));
                }
            }
        }
        else
        {
            for (int i = 0; i < numberOfEnemies; ++i)
            {
                StartCoroutine(Spawn(i, spawners[(int)Mathf.Repeat(i,4)]));
            }
        }
    }

    IEnumerator Spawn(float waitTime, EnemySpawner spawner)
    {
        yield return new WaitForSeconds(waitTime);
        spawner.SpawnEnemy(enemyTarget);
    }

    void SetWaveState()
    {
        if (enemies.Count == 0)
        {
            waveOver = true;
        }
        else
        {
            waveOver = false;
        }
    }

    public static void Reload()
    {
        DeleteAll();

        waveNumber = 0;
        currentWaveTime = 0;
    }

    static void DeleteAll()
    {
        foreach (EnemyBehavior enemy in deadEnemies)
        {
            enemies.Remove(enemy);
            GameObject.Destroy(enemy.gameObject);
        }
        deadEnemies.Clear();

        foreach (EnemyBehavior enemy in enemies)
        {
            GameObject.Destroy(enemy.gameObject);
        }
        enemies.Clear();
        spawners.Clear();
    }
}