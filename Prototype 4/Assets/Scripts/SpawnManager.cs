using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    public float spawnRange = 10;
    private int enemyCount;
    public int waveCount = 1;


    // Start is called before the first frame update
    void Start()
    {
        //SpawnEnemyWave(waveCount);
        //Instantiate(powerUpPrefab, GenerateSpawnV(), powerUpPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0)
        {
            waveCount++;
            SpawnEnemyWave(waveCount);
            Instantiate(powerUpPrefab, GenerateSpawnV(), powerUpPrefab.transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemyToSpawn)
    {
        for(int i = 0; i<enemyToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnV(), enemyPrefab.transform.rotation);
        }
    }


    private Vector3 GenerateSpawnV()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(spawnX, 0, spawnZ); ;
    }
}
