using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemPickup : MonoBehaviour
{
    public LayerMask PowerUp;
    private GameObject player;

    
    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        IncreaseSpeed,
    }
    
    public ItemType type;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(player.transform.position, .2f, PowerUp))
        {
            switch (type)
            {
                case ItemType.ExtraBomb:
                    player.GetComponent<BombController>().bombAmount++;
                    player.GetComponent<BombController>().bombsRemaining++;
                    Destroy(gameObject);
                    break;

                case ItemType.BlastRadius:
                    player.GetComponent<BombController>().explosionSize++;
                    Destroy(gameObject);
                    break;

                case ItemType.IncreaseSpeed:
                    player.GetComponent<PlayerGridMovement>().speed++;
                    Destroy(gameObject);
                    break;

                default:
                    break;
            }
        }
        /*
        if (Physics2D.OverlapCircle(player.transform.position, .2f, PowerUp))
        {
            Debug.Log("exposion");
            player.GetComponent<BombController>().explosionSize++;
            Destroy(gameObject);
        }

        if (Physics2D.OverlapCircle(player.transform.position, .2f, PowerUp))
        {
            Debug.Log("speed");
            player.GetComponent<PlayerGridMovement>().speed++;
            Destroy(gameObject);
        }*/
    }
}
