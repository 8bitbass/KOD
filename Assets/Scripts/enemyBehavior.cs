using UnityEngine;
using System.Collections;

public class enemyBehavior : MonoBehaviour
{
    //member variables
    public float speed, lifeTimer;
    public GameObject dest;

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
        if (Physics.Raycast(transform.position, Vector3.down))
        {
			if ((dest.transform.position - transform.position).magnitude > 0.65f)
			{
				Vector3 newPosition = transform.position + (dest.transform.position - transform.position).normalized * speed * Time.deltaTime;
				GetComponent<Rigidbody>().MovePosition(newPosition);
			}
        }

        lifeTimer -= 1 * Time.deltaTime;

        if (lifeTimer <= 0)
        {
            Delete();
        }
        if (transform.position.y <= -100)
        {
            Delete();
        }
    }

    void Delete()
    {
        GameObject.Destroy(gameObject);
		enemySpawner.enemyCount--;
    }
}
