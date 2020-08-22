using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class MazeConstructor : MonoBehaviour
{
    //1
    public bool showDebug;
    private MazeDataGenerator dataGenerator;

    public Tilemap wallsMap;
    public Tilemap groundMap;
    public Tilemap ceillingMap;
    public Tile walls;
    public Tile ground;
    public TileBase ceilling;

    //2
    public int[,] data
    {
        get; private set;
    }

    //3
    void Awake()
    {
        // default to walls surrounding a single empty cell
        data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
        dataGenerator = new MazeDataGenerator();
    }

    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }

        data = dataGenerator.FromDimensions(sizeRows, sizeCols);
        RenderMap(data, wallsMap, walls, 2);
        RenderMap(data, groundMap, ground, 0);
        RenderMap(data, ceillingMap, ceilling, 1);
    }

    public void RenderMap(int[,] map, Tilemap tilemap, TileBase tile, int id)
    {
        //Clear the map (ensures we dont overlap)
        tilemap.ClearAllTiles();
        int rMax = map.GetUpperBound(0);
        int cMax = map.GetUpperBound(1);
        //Debug.Log(rMax + "/" + cMax);
        //Loop through the width of the map
        for (int x = 0 ; x <= cMax; x++)
        {
            //Loop through the height of the map
            for (int y = rMax; y >= 0; y--)
            {
                // 1 = tile, 0 = no tile
                if (map[y, x] == id)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
        AstarPath.active.Scan();
    }

    void OnGUI()
    {
        //1
        if (!showDebug)
        {
            return;
        }

        //2
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";
        Debug.Log(rMax + "/" + cMax);

        //3
        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    msg += "....";
                }
                else
                {
                    msg += "==";
                }
            }
            msg += "\n";
        }

        //4
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }
}