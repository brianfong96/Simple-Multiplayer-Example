﻿using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{

    public GameObject EnemyPrefab;
    public int NumberOfEnemies;
    public int CurrentEnemies;

    public override void OnStartServer()
    {
        for (int i = 0; i < NumberOfEnemies; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(-8.0f, 8.0f),
                0.0f,
                Random.Range(-8.0f, 8.0f));

            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(0, 180),
                0.0f);

            var enemy = (GameObject)Instantiate(EnemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }
    }

    private void Update()
    {
        
    }

}