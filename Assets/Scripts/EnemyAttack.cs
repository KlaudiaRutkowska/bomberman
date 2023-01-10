using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject player;
    private Animator animator;
    public bool attacking = false;
    public float delay = 1f;
    private float currentRunTime = 0.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentRunTime += Time.deltaTime;

        if (currentRunTime >= delay)
        {
            if (!player.GetComponent<DeathController>().killed)
            {
                if (Vector3.Distance(transform.position, player.transform.position) <= 1f)
                {
                    attacking = true;

                    Vector2 direction = (player.transform.position - transform.position).normalized;
                    animator.SetBool("IsAttacking", true);
                    animator.SetFloat("Vertical", direction.y);
                    animator.SetFloat("Horizontal", direction.x);

                    player.GetComponent<DeathController>().KillPlayer();
                }

                if (attacking && Vector3.Distance(transform.position, player.transform.position) > 1f)
                {
                    attacking = false;
                    animator.SetBool("IsAttacking", false);
                }
            }
            else
            {
                attacking = false;
                animator.SetBool("IsAttacking", false);
            }
            currentRunTime = 0f;
        }
    }
}
