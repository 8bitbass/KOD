﻿using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
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
        if ((dest.transform.position - transform.position).magnitude > 2.65f)
        {
            animState = AnimationState.MOVING;
        }
        else
        {
            animState = AnimationState.IDLE;
        }
        Animation();
    }

    void FixedUpdate()
    {
        if ((dest.transform.position - transform.position).magnitude > 2.65f)
        {

            Vector3 temp = (dest.transform.position - transform.position).normalized;
            temp.y = 0;
            Quaternion tempQ = Quaternion.LookRotation(temp, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, tempQ, Time.fixedDeltaTime * 10);

            Vector3 newPos = transform.localPosition + transform.forward * speed * Time.fixedDeltaTime;
            //Vector3 newPosition = transform.position + (dest.transform.position - transform.position).normalized * speed * Time.deltaTime;
            GetComponent<Rigidbody>().MovePosition(newPos);
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
