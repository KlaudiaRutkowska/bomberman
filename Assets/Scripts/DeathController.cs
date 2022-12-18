using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    private Animator animator;
    public bool killed = false;

    //public bool Killed { get => killed; set => killed = value; }

    private void Awake()
    {
        //grab reference for animation from gameproject
        animator = GetComponent<Animator>();
    }

    public void KillPlayer()
    {
        killed = true;
        GetComponent<BombController>().enabled = false;
        animator.SetBool("Dead", true);
    }

    public void KillEnemy()
    {
        killed = true;
        animator.SetBool("Dead", true);
    }
}
