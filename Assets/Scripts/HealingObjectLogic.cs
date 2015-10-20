using UnityEngine;
using System.Collections;

public class HealingObjectLogic : MonoBehaviour
{
    public float regenAmount = 1;
    public float regenTime = 1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        CharacterHealthLogic temp = collision.gameObject.GetComponent<CharacterHealthLogic>();
        if (temp != null)
        {
            temp.NewHealer(regenAmount, regenTime);
        }
    }
}
