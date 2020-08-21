using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(30, 0, 0);
    private float start = 2;
    private float delay = 2;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", start, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.isOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
