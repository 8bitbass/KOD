using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterHealthLogic : MonoBehaviour
{
    public int maxHealth = 100;

    int currentHealth;

    class updateHealth
    {
        public float amount;
        public float timer;
        public float currentTimer;
        public bool active = true;
    }

    List<updateHealth> heal = new List<updateHealth>();
    List<updateHealth> damage = new List<updateHealth>();
    
    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        CalcHealth();
    }

    void CalcHealth()
    {
        foreach (updateHealth i in heal)
        {
            if (i.active)
            {
                i.currentTimer += Time.deltaTime;
                if (i.currentTimer > i.timer)
                {
                    i.active = false;
                }
                float newHealth = (Time.deltaTime) * i.amount;
            }
        }
    }

    void NewHealer()
    {

    }

    void NewDamage()
    {

    }
}
