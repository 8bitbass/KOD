using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterHealthLogic : MonoBehaviour
{
    public int maxHealth = 100;

    public string deathAnimation = null;

    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public bool isDead = false;

    class updateHealth
    {
        public float amount;
        public float timer;
        public float healthTimer;
        public bool active = true;
    }

    //public enum Elements
    //{
    //    WATER,
    //    EARTH,
    //    FIRE
    //}

    //public List<Elements> elements = new List<Elements>();

    List<updateHealth> healthChange = new List<updateHealth>();
    public enum DeathType
    {
        DESPAWN,
        ANIMATION,
        EXTERNAL
    }

    public DeathType death = DeathType.EXTERNAL;
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
        else if(currentHealth < maxHealth && currentHealth > 0)
        {
            currentHealth += Time.deltaTime * 5;
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
                i.healthTimer -= Time.deltaTime;
                i.timer -= Time.deltaTime;

                if (i.healthTimer <= 0)
                {
                    i.healthTimer += 1;
                    currentHealth += i.amount;
                }

                if (i.timer <= 0)
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
        switch (death)
        {
            case DeathType.DESPAWN:
                isDead = true;
                GameObject.Destroy(gameObject);
                break;
            case DeathType.ANIMATION:
                isDead = true;
                break;
            case DeathType.EXTERNAL:
                isDead = true;
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
            updateHealth temp = new updateHealth
            {
                amount = regenAmount/regenTime,
                timer = regenTime
            };
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
            updateHealth temp = new updateHealth
            {
                amount = -(damageAmount/damageTime),
                timer = damageTime
            };
            healthChange.Add(temp);
        }
    }
}
