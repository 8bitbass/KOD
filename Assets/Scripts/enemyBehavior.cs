using UnityEngine;
using System.Collections;

public class enemyBehavior : MonoBehaviour
{
	//member variables
	public float speed;
	public GameObject dest;
	private Animator anim;

	private enum AnimationState
	{
		IDLE,
		MOVING,
		ATTACKING,
		HURT,
		DYING
	}

	private AnimationState animState = AnimationState.IDLE;
	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		//GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(Random.Range(0.0f, 255.0f) / 255, Random.Range(0.0f, 255.0f) / 255, Random.Range(0.0f, 255.0f) / 255));
	}

	// Update is called once per frame
	void Update()
	{
		Animation();
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

		if (transform.position.y <= -100)
		{
			Delete();
		}
	}

	void Delete()
	{
		GameObject.Destroy(gameObject);
	}

	void Animation()
	{

		switch (animState)
		{
			case AnimationState.ATTACKING:
				anim.SetTrigger("Attacking");
				break;
			case AnimationState.DYING:
				anim.SetTrigger("Dying");
				break;
			case AnimationState.HURT:
				anim.SetTrigger("Hurting");
				break;
			case AnimationState.IDLE:
				anim.SetTrigger("Idle");
				break;
			case AnimationState.MOVING:
				anim.SetTrigger("Moving");
				break;
		}
	}
}
