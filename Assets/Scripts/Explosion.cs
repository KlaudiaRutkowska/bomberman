using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        //grab reference for animation from gameproject
        animator = GetComponent<Animator>();
    }

    public void setActiveAnimation(string animation)
    {
        animator.SetBool("Middle", animation == "middle");
        animator.SetBool("End", animation == "end");
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
