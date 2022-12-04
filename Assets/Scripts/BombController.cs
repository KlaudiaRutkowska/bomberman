using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public float bombFuseTime = 5f;
    public int bombAmount = 1;
    private int bombsRemaining;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public float explosionDuration = 1f;
    public int explosionSize = 1;

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }
    
    // Update is called once per frame
    void Update()
    {
        //if number of bombs remaining is grater than 0 and put space key code
        if (bombsRemaining > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            //start coroutine of function
            StartCoroutine(PlaceBomb());
        }
    }

    //special method type where placing bomb is in kinda loop? after all instructions here the old bomb dissapears
    //and we can put bomb again
    private IEnumerator PlaceBomb()
    {
        //save to new variable position from player
        Vector2 position = transform.position;

        //round coordinates x, y so bomb is put on whole positions
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        //create instance of bomb with bomb prefab and rounded position
        //so bomb can appear there
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;

        //wait for bomb to fuse for specific amount of seconds
        yield return new WaitForSeconds(bombFuseTime);

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);

        Destroy(explosion.gameObject, explosionDuration);

        Explode(position, Vector2.up, explosionSize);
        Explode(position, Vector2.down, explosionSize);
        Explode(position, Vector2.left, explosionSize);
        Explode(position, Vector2.right, explosionSize);

        Destroy(bomb);
        //after that bom can be put again
        bombsRemaining++;
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }
    }
}
