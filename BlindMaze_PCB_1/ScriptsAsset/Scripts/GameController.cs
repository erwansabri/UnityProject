using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;
using System.Collections;

[RequireComponent(typeof(MazeConstructor))]               // 1


public class GameController : MonoBehaviour
{
    private MazeConstructor generator;
    public int x, y;
    public AstarPath astar;
    public ObjectifSpawner objectifSpawner;
    public GameObject player;
    PlayerStats playerStats;
    PlayerMovement playerMovement;


    IEnumerator Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        playerStats = player.GetComponent<PlayerStats>();
        generator = GetComponent<MazeConstructor>();      // 2
        generator.GenerateNewMaze(y, x);

        yield return new WaitForSeconds(.1f);

        objectifSpawner.PlaceObjectif();
        AstarPath.active.Scan();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || playerStats.isAlive == false || playerMovement.isLevelDone)
        {
            Application.Quit();
            GenerateNewMaze();
        }
    }

    void GenerateNewMaze()
    {
        Debug.Log("Niveau fini");
    }
}