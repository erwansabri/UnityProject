using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapShadowCaster : MonoBehaviour
{
    public Tilemap map;
    public GameObject shadowTile;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer rend = shadowTile.GetComponent<SpriteRenderer>();
        Vector3 tileSize = rend.bounds.size;

        foreach (var pos in map.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = map.CellToWorld(localPlace);
            if (map.HasTile(localPlace))
            {
                place.x += tileSize.x / 2;
                place.y += tileSize.y / 2;
                Instantiate(shadowTile, place, Quaternion.identity);
            }
        }

    }
}
