using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterHealthLogic : MonoBehaviour
{
    public int maxHealth = 100;

    private float currentHealth;

    class updateHealth
    {
        public float amount;
        public float totalTime;
        public float timer;
        public float healthTimer;
        public bool active = true;
    }

    List<updateHealth> healthChange = new List<updateHealth>();
    public enum DeathType
    {
        DESPAWN,
        ANIMATION
    }

    public DeathType Death;
    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        CalcHealth();

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            Die();
        }

        //Debug.Log(currentHealth);
    }

    void CalcHealth()
    {
        foreach (updateHealth i in healthChange)
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

                if (i.timer >= i.totalTime)
                {
                    i.active = false;
                    i.healthTimer = 0;
                    i.timer = 0;
                }
            }
        }
    }

    void Die()
    {
        switch (Death)
        {
            case DeathType.DESPAWN:
                GameObject.Destroy(gameObject);
                break;
            case DeathType.ANIMATION:
                break;
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
            healthChange.Add(temp);
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
            healthChange.Add(temp);
        }
    }
}
