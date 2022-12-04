using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject bombPrefab;
    public float bombFuseTime = 5f;
    public int bombAmount = 1;
    private int bombsRemaining;

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

        Destroy(bomb);
        //after that bom can be put again
        bombsRemaining++;
    }
}
