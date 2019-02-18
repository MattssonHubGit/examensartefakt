using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WaveLoadout : ScriptableObject {


    [Header("What EnemiesToSpawn to spawn?")]
    public GameObject[] enemyPrefabs;

    [Header("Where to spawn?")]
    public SpawnAreas[] areasToSpawnIn;
    public enum SpawnAreas { NORTH, EAST, SOUTH, WEST}   

    [Header("How long between enemy spawns?")]
    [Range(0f, 10f)] public float spawnAfterSecondsFromLatestMin;
    [Range(0f, 10f)] public float spawnAfterSecondsFromLatestMax;

}
