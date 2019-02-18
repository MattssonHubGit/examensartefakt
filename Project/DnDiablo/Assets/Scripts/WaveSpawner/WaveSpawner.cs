﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class WaveSpawner : MonoBehaviour {


    public enum SpawnStateWave { SPAWNING, WAITING, COUNTING };

    [Header("Waves")]
    public WaveLoadout[] waves;
    private int nextWave = 0;

    [Header("Time between waves")]
    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] northSpawnPoints;
    [SerializeField] private Transform[] eastSpawnPoints;
    [SerializeField] private Transform[] southSpawnPoints;
    [SerializeField] private Transform[] westSpawnPoints;

    private int enemiesToSpawnCount;
    
    private float scanFrequency = 0.5f;
    private float waveTimeToSpawn;
    private float waveTimeToSpawnTimeStamp;
    private SpawnStateWave currentState = SpawnStateWave.COUNTING;
    
    [HideInInspector] private int currentWaveCounter = 0;

    public int CurrentWaveItteration
    {
        get
        {
            return currentWaveCounter;
        }
    }

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (currentState == SpawnStateWave.WAITING)
        {
            //Debug.Log("WAITING");
            if (!EnemyIsAliveRandom())
            {
                WaveCompletedRandom();
            }
            else
            {
                //Debug.Log(EnemyIsAlive().ToString());
                return;
            }
        }

        //Start new wave after countdown
        if (waveCountdown <= 0)
        {
            if (currentState != SpawnStateWave.SPAWNING)
            {

                currentWaveCounter++; //Increase counter

                StartCoroutine(SpawnWave(waves[nextWave])); //Start Spawning the next wave

            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }


    //When a wave is completed, change to the next wave - if all waves are completed currently debug "complete victory"
    private void WaveCompletedRandom()
    {
        Debug.Log("Wave completed");

        currentState = SpawnStateWave.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1) //Change the code in here for when all waves of a level is completed
        {
            Debug.Log("All waves complete - Complete Victory!");
            //Finish level here

        }
        else
        {
            nextWave++;
        }
    }

    //Check once every 5s if there are enemies still alive
    private bool EnemyIsAliveRandom()
    {
        Debug.LogError("WaveSpawner::EnemyIsAliveRandom() -- Function is using || if(GameObject.FindGameObjectWithTag('enemy') == null) || This has to change!");

        scanFrequency -= Time.deltaTime;
        if (scanFrequency <= 0f)
        {
            scanFrequency = 5f;
            if (GameObject.FindGameObjectWithTag("enemy") == null) 
            {
                return false;
            }
        }
        return true;
    }


    //Spawn each enemy in the current wave, then start waiting for the player to un-alive them.
    IEnumerator SpawnWave(WaveLoadout _wave)
    {
        currentState = SpawnStateWave.SPAWNING;
        float nextSpawn = Random.Range(_wave.spawnAfterSecondsFromLatestMin, _wave.spawnAfterSecondsFromLatestMax);

        for (int i = 0; i < _wave.enemyPrefabs.Length; i++)
        {
            SpawnEnemy(_wave.enemyPrefabs[i], DetermineSpawnPosition(_wave.areasToSpawnIn));


            //Wait for random amount of seconds before spawning a new enemy
            yield return new WaitForSeconds(Mathf.Abs(Random.Range(_wave.spawnAfterSecondsFromLatestMin, _wave.spawnAfterSecondsFromLatestMax)));
        }

        currentState = SpawnStateWave.WAITING;
        yield break;
    }


    private Transform DetermineSpawnPosition(WaveLoadout.SpawnAreas[] _spawnAreas)
    {
        //Choose at random what area to spawn in
        //Then chose a random point in that area to spawn at

        int randomArea = (int)Random.Range(0, _spawnAreas.Length); //What area

        for (int i = 0; i < _spawnAreas.Length; i++)
        {
            if (i == randomArea) //Was this the right area?
            {
                switch (_spawnAreas[i]) //Spawn at a random point within this area
                {
                    case WaveLoadout.SpawnAreas.NORTH:
                        {
                            return PickPointInArea(northSpawnPoints);

                            break;
                        }
                    case WaveLoadout.SpawnAreas.EAST:
                        {
                            return PickPointInArea(eastSpawnPoints);

                            break;
                        }
                    case WaveLoadout.SpawnAreas.SOUTH:
                        {
                            return PickPointInArea(southSpawnPoints);

                            break;
                        }
                    case WaveLoadout.SpawnAreas.WEST:
                        {
                            return PickPointInArea(westSpawnPoints);

                            break;
                        }
                    default:
                        Debug.LogError("WaveSpawner::EnemyIsAliveRandom() -- Reached end of state-machine, did more areas get added without updating the randomizer? Returning self as spawnpoint");
                        break;
                }
            }
        }

        Debug.LogError("WaveSpawner::EnemyIsAliveRandom() -- State-machine did not return a spawnpoint, did more areas get added without updating the randomizer? Returning self as spawnpoint");
        return this.transform;

    }

    private Transform PickPointInArea(Transform[] pointsInArea)
    {
        int randomPoint = (int)Random.Range(0, pointsInArea.Length);

        for (int j = 0; j < pointsInArea.Length; j++)
        {
            if (j == randomPoint)
            {
                return pointsInArea[j];
            }
        }

        //If some error occurs, return point 0
        Debug.LogError("WaveSpawner::PickPointInArea() -- Reached bottom of function, returning default 0 area");
        return pointsInArea[0];
    }
    private void SpawnEnemy(GameObject _enemy, Transform _spawnPosition)
    {
        GameObject enemyObj = Instantiate(_enemy, _spawnPosition.position, _enemy.transform.rotation);

        //I hate this bit, it's here due to a bug where the spawned agent does not know where they are if simply instantiated
        if (enemyObj.GetComponent<NavMeshAgent>() != null)
        {
            enemyObj.GetComponent<NavMeshAgent>().Warp(_spawnPosition.position);
        }
    }

}