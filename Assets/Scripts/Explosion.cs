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
        //atan2 function returnsangle in radians
        float angle = Mathf.Atan2(direction.y, direction.x);
        //calculate rotation and rotate in z axis
        //angleAxis functions requires angle in degrees so it must be changed by * rad2deg
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}