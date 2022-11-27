using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerGridMovement : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float speed = 1;
    public Transform movePoint;
    public LayerMask obstacles;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    private void Awake()
    {
        //grab reference for animation from gameproject
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //make player go towards child's position
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

        //if distance between parent and child is 0
        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            //set animation parameter
            
            animator.SetBool("IsWalking", Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f);
            animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
            animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));

            //if vertical inputs are put
            if (math.abs(Input.GetAxisRaw("Vertical")) == 1)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), .2f, obstacles))
                {
                    //change child's position
                    movePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                }
            }

            //if horizontal inputs are put
            if (math.abs(Input.GetAxisRaw("Horizontal")) == 1)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), .2f, obstacles))
                {
                    //change child's position
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                }
            }
        }
    }
}