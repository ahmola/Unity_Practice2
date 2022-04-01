using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RabbitController : MonoBehaviour
{
    public float turnSpeed = 20.0f;
    public float moveSpeed = 15.0f;
    public CanvasGroup exitBackGroundText;
    public Button againButton;
    public float fadeDuration = 2.0f;
    public float displayTextDuration = 2.0f;

    bool isCollide;
    float r_Timer;
    Animator r_Animator;
    Rigidbody r_Rigidbody;
    Vector3 r_Movement;
    Quaternion r_Rotation = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        isCollide = false;
        r_Animator = GetComponent<Animator>();
        r_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical= Input.GetAxis("Vertical");

        r_Movement.Set(horizontal, 0f, vertical);
        r_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isRunning = hasHorizontalInput || hasVerticalInput;
        r_Animator.SetBool("IsRunning", isRunning);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, r_Movement, turnSpeed * Time.deltaTime, 0f);
        r_Rotation = Quaternion.LookRotation(desiredForward);

        if (isCollide)
        {
            r_Timer += Time.deltaTime;
            exitBackGroundText.alpha = r_Timer / fadeDuration;
            if (r_Timer > fadeDuration + displayTextDuration)
            {
                // againButton   
            }
        }
    }

    private void OnAnimatorMove()
    {
        r_Rigidbody.MovePosition(r_Rigidbody.position + r_Movement * r_Animator.deltaPosition.magnitude * moveSpeed);
        r_Rigidbody.MoveRotation(r_Rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
      if(collision.gameObject.tag == "Rock")
        {
            isCollide = true;
        }
    }
}
