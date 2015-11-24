using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    //member variables
    public GameObject enemy;

    // Use this for initialization
    void Start()
    {      
        EnemyManagerLogic.spawners.Add(this);
    }

    public void SpawnEnemy(GameObject target)
    {
        GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity) as GameObject;
        EnemyBehavior newSpawn = newEnemy.GetComponent<EnemyBehavior>();

        if (newSpawn != null)
        {
            newSpawn.dest = target;
            newSpawn.points = (EnemyManagerLogic.difficultyLevel * 5) + (EnemyManagerLogic.waveNumber/ 10);
            //enemies.Add(newSpawn);
        }
    }


}
