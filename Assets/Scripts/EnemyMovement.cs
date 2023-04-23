using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.Mathematics;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    public Transform movePoint;
    public LayerMask destructible, undestructible, bombs, explosion, player;

    public Vector3 position;
    public float speed = 1;
    public float delay = .5f;
    public float currentRunTime = 0.0f;

    //private string[] directions = { "Horizontal", "Vertical" };
    //private int[] steps = { -1, 0, -1, -1, 0, 1, 1, 0, 1 };

    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //if enemy is not killed AND not attacking he can move
        if (!GetComponent<DeathController>().killed && !GetComponent<EnemyAttack>().attacking)
        {
            //make player go towards child's position
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

            currentRunTime += Time.deltaTime;

            if (currentRunTime >= delay)
            {
                //if distance between parent and child is 0
                if (Vector3.Distance(transform.position, movePoint.position) <= 0f)
                {
                    generateRandomPosition();

                    if (
                            !Physics2D.OverlapCircle(movePoint.position + position, .2f, destructible) &&
                            !Physics2D.OverlapCircle(movePoint.position + position, .2f, undestructible) &&
                            !Physics2D.OverlapCircle(movePoint.position + position, .2f, bombs) &&
                            !Physics2D.OverlapCircle(movePoint.position + position, .2f, player) &&
                            position != (movePoint.position - position)
                        )
                    {
                        //change child's position
                        movePoint.position += position;
                    }
                }
                currentRunTime = 0f;
            }

            //if distance between player position and move point position is grater or equal to zero
            //equal because if we put the bomb and we stay in the place where will be explosion,
            //there distance between player and move point  will be 0
            if (Vector3.Distance(transform.position, movePoint.position) >= 0f)
            {
                //if between player and explosion distance will be equal or less than 2f,
                if (Physics2D.OverlapCircle(transform.position, .2f, explosion))
                {
                    //then player dies
                    GetComponent<DeathController>().KillEnemy();
                    Destroy(gameObject, 2.5f);
                }
            }
        }
    }

    void generateRandomPosition()
    {
        //Vector3 occupiedPosition = new Vector3(0, 0, 0);
        List<Vector3> occupiedPositions = new List<Vector3>();

        do
        {
            /*
            string direction = directions[UnityEngine.Random.Range(0, directions.Length)];
            int step = steps[UnityEngine.Random.Range(0, steps.Length)];

            switch (direction)
            {
                case "Horizontal":
                    position = new Vector3(step, 0f, 0f);
                    break;
                case "Vertical":
                    position = new Vector3(0f, step, 0f);
                    break;
                default:
                    break;
            }
            */
            
            if (position.x == 0f)
            {
                position.y = UnityEngine.Random.Range(-1, 2);
            }

            if (position.y == 0f)
            {
                position.x = UnityEngine.Random.Range(-1, 2);
            }
            
            if (
                    Physics2D.OverlapCircle(movePoint.position + position, .2f, destructible) ||
                    Physics2D.OverlapCircle(movePoint.position + position, .2f, undestructible) ||
                    Physics2D.OverlapCircle(movePoint.position + position, .2f, bombs) ||
                    Physics2D.OverlapCircle(movePoint.position + position, .2f, player)
                )
            {
                occupiedPositions.Add(position);
            }
        } while (occupiedPositions.Contains(position));

        //Debug.Log(position);
    }
}
