using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
	[Header("Bomb")]
	public GameObject bombPrefab;
	public float bombFuseTime = 5f;
	public int bombAmount = 1;
	public int bombsRemaining;

	[Header("Explosion")]
	public Explosion explosionPrefab;
	public LayerMask explosionLayerMask;
	public float explosionDuration = .5f;
	public int explosionSize = 1;

	public Tilemap destructibleTileMaps;

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
		Vector2 bombPosition = transform.position;

		//round coordinates x, y so bomb is put on whole positions
		bombPosition.x = Mathf.Round(bombPosition.x);
		bombPosition.y = Mathf.Round(bombPosition.y);

		//create instance of bomb with bomb prefab and rounded position
		//so bomb can appear there
		GameObject bomb = Instantiate(bombPrefab, bombPosition, Quaternion.identity);
		bombsRemaining--;

		//wait for bomb to fuse for specific amount of seconds
		yield return new WaitForSeconds(bombFuseTime);
		
		//create explosion instance (with type Explosion which is class)
		//which contains explosion prefab created in unity,
		//position, which is basicly player position rounded (line 45, 46)

		//this is explosion with "start" animation because beside of current position
		//we don't need explosion size. "start" animation is always inside,
		//and "start" animation is set as default in animator.
		Explosion explosion = Instantiate(explosionPrefab, bombPosition, Quaternion.identity);

		CheckIfPlayerCanBeKilled(bombPosition);

		//destroy explosion after specific amount of time saved in variable explosionDuration
		Destroy(explosion.gameObject, explosionDuration);

		//cal method Explode with parameters: current position,
		//direction (in which side flip explosion tiles), explosion size 
		Explode(bombPosition, Vector2.up, explosionSize);
		Explode(bombPosition, Vector2.down, explosionSize);
		Explode(bombPosition, Vector2.left, explosionSize);
		Explode(bombPosition, Vector2.right, explosionSize);
		
		Destroy(bomb);
		//after that bom can be put again
		bombsRemaining++;
	}

	//recursive method
	private void Explode(Vector2 explosionPosition, Vector2 direction, int length)
	{
		//exit condition
		if (length <= 0)
		{
			return;
		}

		//add direction to current explosion position
		explosionPosition += direction;

		//if explosion position overlaps undestructible layer mask
		//then stop destroying in that direction
		if (Physics2D.OverlapBox(explosionPosition, Vector2.one / 2f, 0f, explosionLayerMask))
		{
			//fet rid of destructible tile map at current position 
			StartCoroutine(DestroyDestructibleTileMap(explosionPosition));
			return;
		}

		//create instance explosion with current position 
		//(which is changed in line 89)
		Explosion explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
		//using method from Explosion class we check if length of explosion is grater
		//than 1 or not and depending on it set "middle" or "end" explosion animation 
		explosion.setActiveAnimation(length > 1 ? "middle" : "end");
		explosion.SetDirection(direction);

		CheckIfPlayerCanBeKilled(explosionPosition);

		//end explosion after certain amount of time
		explosion.DestroyAfter(explosionDuration);

		//call again method with reduced length by 1
		Explode(explosionPosition, direction, length - 1);
	}

	private IEnumerator DestroyDestructibleTileMap(Vector2 position)
	{
		//get the cell from tilemap
		//it's needed to be converted from world position to cell position
		Vector3Int cell = destructibleTileMaps.WorldToCell(position);
		//get tile from the cell position
		TileBase tile = destructibleTileMaps.GetTile(cell);

        //if destructible tile map exists
        if (tile != null)
		{
            yield return new WaitForSeconds(explosionDuration);
            //then, get rid of it
            destructibleTileMaps.SetTile(cell, null);
		}
	}

	public void CheckIfPlayerCanBeKilled(Vector2 position)
	{
        //we need to save player position in vector type so we can sompare it with parameter position of this method
        Vector2 playerPosition = new Vector2(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));

		if (position == playerPosition)
		{
			GetComponent<DeathController>().KillPlayer();
		}
	}
}
