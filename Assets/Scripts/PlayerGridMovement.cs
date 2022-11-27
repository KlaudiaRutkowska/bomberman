using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGridMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    [SerializeField] private float speed = 1;
    //[SerializeField] private float axis = 0;

    private void Awake()
    {
        //grab reference for rigidbody from gameproject
        body = GetComponent<Rigidbody2D>();
        //grab reference for animation from gameproject
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //axis = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);

        //set animation parameter
        animator.SetBool("IsWalking", Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
    }
}
