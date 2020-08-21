using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] animalPrefabs;
    private float xSpawn = 20;
    private float zSpawn = 20;
    private float startSpawn = 2;
    private float timeSpawn = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAnimals", startSpawn, timeSpawn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnAnimals()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-xSpawn, xSpawn), 0, zSpawn);

        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
    }
}
