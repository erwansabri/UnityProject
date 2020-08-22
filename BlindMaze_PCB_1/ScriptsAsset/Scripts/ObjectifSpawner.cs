using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectifSpawner : MonoBehaviour
{
    public Tilemap wallMap;
    public GameObject objectif;
    bool isFound = false;



    public void PlaceObjectif()
    {
        for (int i = wallMap.size.x - 1 ; i >  1 ; i--)
        {
            if (isFound)
                break;
            for(int j = wallMap.size.y - 1 ; j > 1; j--)
            {
                if(!(wallMap.HasTile(new Vector3Int(i+1, j, 0)) || wallMap.HasTile(new Vector3Int(i-1, j, 0)) || wallMap.HasTile(new Vector3Int(i+1, j+1, 0)) || wallMap.HasTile(new Vector3Int(i-1, j+1, 0)) || wallMap.HasTile(new Vector3Int(i, j+1, 0)) || wallMap.HasTile(new Vector3Int(i, j-1, 0)) || wallMap.HasTile(new Vector3Int(i+1, j-1, 0)) || wallMap.HasTile(new Vector3Int(i-1, j-1, 0))))
                {
                    Debug.Log("FOUND");
                    Instantiate(objectif, new Vector3(i+.5f, j+.5f, 0), Quaternion.identity);
                    isFound = true;
                    break;
                }
            }
        }
    }
   


}
