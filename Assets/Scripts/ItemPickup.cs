using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject player;
    public LayerMask itemPickup;

    void Update()
    {
        if (Physics2D.OverlapCircle(player.transform.position, .2f, itemPickup))
        {
            player.GetComponent<BombController>().bombAmount++;
            player.GetComponent<BombController>().bombsRemaining++;
            Destroy(gameObject);
        }
    }
}
