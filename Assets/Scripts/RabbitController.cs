using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    public float turnSpeed = 15.0f;
    public float moveSpeed = 2.5f;

    private Quaternion r_Rotation = Quaternion.identity;
    private Animator r_Animator;
    private Vector3 r_Movement;
    private bool r_isDead = false;
    private Rigidbody r_Rigidbody;

    void Start()
    {
        r_Animator = GetComponent<Animator>();
        r_Rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        r_Movement.Set(horizontal, 0f, vertical);
        r_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isRunning = hasHorizontalInput || hasVerticalInput;

        r_Animator.SetBool("IsRunning", isRunning);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, r_Movement, turnSpeed*Time.deltaTime, 0f);
        r_Rotation = Quaternion.LookRotation(desiredForward);
    }

    private void OnAnimatorMove()
    {
        r_Rigidbody.MovePosition(r_Rigidbody.position + r_Movement * r_Animator.deltaPosition.magnitude * moveSpeed);
        r_Rigidbody.MoveRotation(r_Rotation);
    }
}