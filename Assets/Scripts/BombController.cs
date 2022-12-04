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
        if (bombsRemaining > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        Destroy(bomb);
        bombsRemaining++;
    }

    /*
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            collider.isTrigger = false;
        }
    }*/
}
