using UnityEngine;
using System.Collections;

public class enemyBehavior : MonoBehaviour
{
    //member variables
    public float speed;
    public Vector3 dest;

    // Use this for initialization
    void Start()
    {
        GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(Random.Range(0.0f, 255.0f) / 255, Random.Range(0.0f, 255.0f) / 255, Random.Range(0.0f, 255.0f) / 255));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (transform.position.y <= 1.05f)
        {
            Vector3 newPosition = transform.position + (dest - transform.position).normalized * speed * Time.deltaTime;
            GetComponent<Rigidbody>().MovePosition(newPosition);
        }
    }
}
