using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemPickup : MonoBehaviour
{
    public GameObject player;
    public LayerMask itemPickup;
    public Tilemap destructibleTileMaps;

    public enum ItemType
    {
        ExtraBomb,
        BlastRadius,
        IncreaseSpeed,
    }

    public ItemType type;

    void Update()
    {
        if (Physics2D.OverlapCircle(player.transform.position, .2f, itemPickup))
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
    }

    private void GetDestructibleTileMap(Vector2 position)
    {
        //get the cell from tilemap
        //it's needed to be converted from world position to cell position
        Vector3Int cell = destructibleTileMaps.WorldToCell(position);
        //get tile from the cell position
        TileBase tile = destructibleTileMaps.GetTile(cell);

        //if destructible tile map exists
        if (tile != null)
        {
            //then, get rid of it
            destructibleTileMaps.SetTile(cell, null);
        }
    }
}
