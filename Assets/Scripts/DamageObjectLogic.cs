using UnityEngine;
using System.Collections;

public class DamageObjectLogic : MonoBehaviour
{
    public float damageAmount = 1;
    public float damageTime = 1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        CharacterHealthLogic temp = collision.gameObject.GetComponent<CharacterHealthLogic>();
        if (temp != null)
        {
            temp.NewDamage(damageAmount, damageTime);
        }
    }
}
