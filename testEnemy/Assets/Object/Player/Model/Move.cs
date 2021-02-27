using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 velocity;
    [SerializeField]
    private float gravity=0.98f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {

        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float RunSpeed;
        RunSpeed = 1.0f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            RunSpeed = 2.0f;
        }

        if (characterController.isGrounded)
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));


            if (velocity.magnitude > 0.1f)
            {
                animator.SetFloat("Speed", velocity.magnitude*RunSpeed);
                transform.LookAt(transform.position + velocity);


                
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }
        }
        velocity.x += 0;
        velocity.z += 0;
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * gravity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger("Jump");
        if (Input.GetKeyDown(KeyCode.Z))
            animator.SetTrigger("Atack");
    }

}

