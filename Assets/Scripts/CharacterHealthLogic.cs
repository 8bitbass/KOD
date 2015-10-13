using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterHealthLogic : MonoBehaviour
{
    public int maxHealth = 100;

    float currentHealth;

    class updateHealth
    {
        public float amount;
        public float totalTime;
        public float timer;
        public float healthTimer;
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
                i.healthTimer += Time.deltaTime;
                i.timer += Time.deltaTime;

                if (i.healthTimer >= 1)
                {
                    i.healthTimer -= 1;
                    currentHealth += i.amount;
                }

                if(i.timer >= i.totalTime)
                {
                    i.active = false;
                    i.healthTimer = 0;
                    i.timer = 0;
                }
            }
        }
    }

    public void NewHealer(float regenAmount, float regenTime)
    {
        if (regenTime < 1)
        {
            currentHealth += regenAmount;
        }
        else
        {
            updateHealth temp = new updateHealth();
            temp.amount = regenAmount / regenTime;
            temp.totalTime = regenTime;
        }
    }

    public void NewDamage(float damageAmount, float damageTime)
    {
        if (damageTime < 1)
        {
            currentHealth -= damageAmount;
        }
        else
        {
            updateHealth temp = new updateHealth();
            temp.amount = -(damageAmount / damageTime);
            temp.totalTime = damageTime;
        }
    }
}
