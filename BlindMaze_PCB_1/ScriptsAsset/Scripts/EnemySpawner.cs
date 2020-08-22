using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public Tilemap ceilingMap;
    public GameObject enemy;
    public int numberOfEnemies;


    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.1f);
        //numberOfEnemies = ceilingMap.size.x /3 ;
        PlaceEnemies();
    }

    void PlaceEnemies()
    {
        for(int i = 0; i < ceilingMap.size.x; i++)
        {
            for (int j = 0; j < ceilingMap.size.y; j++)
            {
                if (!(ceilingMap.HasTile(new Vector3Int(i, j, 0))) && numberOfEnemies != 0 && (Random.value < .05f ? true : false))
                {
                    Instantiate(enemy, new Vector2(i+.5f, j+.5f), Quaternion.identity);
                    numberOfEnemies--;
                }
            }
        }
    }
}
