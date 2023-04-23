using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PowerUpGenerator : MonoBehaviour
{
    public Tilemap destructibleTileMap;

    private GameObject[] powerUpPrefabs;
    private List<Vector3> tileWorldLocations;
    private int randomIndex;

    void Start()
    {
        //Load files from Resources/PowerUps folder to array
        powerUpPrefabs = Resources.LoadAll<GameObject>("PowerUps");
        tileWorldLocations = new List<Vector3>();

        //foreach over allPositionsWithin destructible tilemap
        foreach (var position in destructibleTileMap.cellBounds.allPositionsWithin)
        {
            Vector3Int location = new Vector3Int(position.x, position.y, position.z);
            Vector3 place = destructibleTileMap.CellToWorld(location);
            Vector3 fixedLocation = new Vector3(place.x + 0.5f, place.y + 0.5f, 0);

            //check if destructible tilemap has tile in position
            if (destructibleTileMap.HasTile(location))
            {
                //add position to List of tiles positions
                tileWorldLocations.Add(fixedLocation);
            }
        }

        //list of used indexes of positions in tilemap
        List<int> usedIndexes = new List<int>();

        if (tileWorldLocations.Count > 0 && powerUpPrefabs.Length > 0)
        {
            for (int i = 0; i < powerUpPrefabs.Length; i++)
            {
                do 
                {
                    //get random index from positions in tilemap
                    randomIndex = Random.Range(0, tileWorldLocations.Count);
                } 
                //check if usedIndexes contains randomIndex
                while (usedIndexes.Contains(randomIndex));

                Instantiate(powerUpPrefabs[i], tileWorldLocations[randomIndex], Quaternion.identity);

                //add randomIndex to usedIndexes to prevent powerUp generate in same location
                usedIndexes.Add(randomIndex);
            }
        }
    }
}