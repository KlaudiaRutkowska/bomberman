using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.Mathematics;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    private Animator animator;
    public Transform movePoint;
    public LayerMask destructible, undestructible, bombs, explosion;

    public float speed = 1;
    public float delay = 1f;

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
    void Update()
    {
        if (!GetComponent<DeathController>().killed)
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

                generateRandomPosition();

                /*
                //if vertical inputs are put
                if (math.abs(Input.GetAxisRaw("Vertical")) == 1)
                {
                    if (
                            !Physics2D.OverlapCircle(movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), .2f, destructible) &&
                            !Physics2D.OverlapCircle(movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), .2f, undestructible) &&
                            !Physics2D.OverlapCircle(movePoint.position + new Vector3(0, Input.GetAxisRaw("Vertical"), 0), .2f, bombs)
                        )
                    {
                        //change child's position
                        movePoint.position += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                    }
                }

                //if horizontal inputs are put
                if (math.abs(Input.GetAxisRaw("Horizontal")) == 1)
                {
                    if (
                            !Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), .2f, destructible) &&
                            !Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), .2f, undestructible) &&
                            !Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), .2f, bombs)
                        )
                    {
                        //change child's position
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                    }
                }
                */
            }

            //if distance between player position and move point position is grater or equal to zero
            //equal because if we put the bomb and we stay in the place where will be explosion,
            //there distance between player and move point  will be 0
            //grater because ?
            if (Vector3.Distance(transform.position, movePoint.position) >= 0f)
            {
                //if between player and explosion distance will be equal or less than 2f,
                if (Physics2D.OverlapCircle(transform.position, .2f, explosion))
                {
                    //then player dies
                    GetComponent<DeathController>().Kill();
                }
            }
        }
    }

    void generateRandomPosition()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);

        position.x = Random.Range(-1, 2);
        position.y = Random.Range(-1, 2);

        if (position.x == 0f)
        {
            position.y = Random.Range(-1, 2);
        }
        else
        {
            position.y = 0f;
        }


        if (position.y == 0f)
        {
            position.x = Random.Range(-1, 2);
        }
        else
        {
            position.x = 0f;
        }
    }
}
