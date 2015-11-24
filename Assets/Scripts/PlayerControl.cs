using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{
    //OnAnimatorMove might be required later on.
    public float speed = 6.1f;
    public float directionSpeed = 4f;
    public float rotationDegreesPerSecond = 120f;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private float r = 0.0f;

    void Update()
    {
        Animator dave = GetComponent<Animator>();

        if (Input.GetAxis("Fire1") != 0f)
        {
            dave.SetBool("Attack", true);
        }
        else
        {
            dave.SetBool("Attack", false);
            
        }
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 stickDirection = new Vector3(horizontal, 0, vertical);
        r = stickDirection.sqrMagnitude;
        if (r > 1)
        {
            r = stickDirection.normalized.sqrMagnitude;
        }

        Vector2 dir = new Vector2(horizontal, vertical).normalized;

        float deg = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        Quaternion endRotation = Quaternion.Euler(0, deg + Camera.main.transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, r * .2f);

        transform.localPosition += transform.forward * r * speed * Time.deltaTime;
    }

    //void FixedUpdate()
    //{
        //if ((direction >= 0 && horizontal >= 0) || (direction < 0 && horizontal < 0))
        //{
        //    Vector3 rotationAmount = Vector3.Lerp(Vector3.zero, new Vector3(0f, rotationDegreesPerSecond * (horizontal < 0f ? -1f : 1f), 0f), Mathf.Abs(horizontal));
        //    Quaternion deltaRotation = Quaternion.Euler(rotationAmount * Time.deltaTime);
        //    this.transform.rotation = (this.transform.rotation * deltaRotation);
        //}
    //}
}