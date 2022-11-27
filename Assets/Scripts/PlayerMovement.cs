using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

        //flipping player depends he moves left or right
        //if player moves right
        if (Input.GetAxis("Horizontal") > 0)
        {
            //set scale on all axis to 1
            transform.localScale = Vector2.one;
        }
        //if player moves left
        else if (Input.GetAxis("Horizontal") < 0)
        {
            //flip player on x axis
            transform.localScale = new Vector2(-1, 1);
        }

        //set animation parameter
        animator.SetBool("run", Input.GetAxis("Horizontal") != 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


}
