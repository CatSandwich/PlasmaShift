using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Helpers;
using States;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public float MinSpawnDelay;
    public float MaxSpawnDelay;
    public float SpawnHeight;
    
    public Enemy[] EnemyPrefabs;
    public UnityEvent<Enemy> EnemySpawned;

    private MainState MainState;
    private readonly List<Enemy> ActiveEnemies = new();

    private IEnumerator Start()
    {
        while (true)
        {
            if (TryPickNewEnemy(out Enemy prefab))
            {
                Vector3 position = transform.position + Vector3.up * Random.Range(-SpawnHeight, SpawnHeight);
                Enemy enemy = Instantiate(prefab, position, Quaternion.identity, transform);
                
                ActiveEnemies.Add(enemy);
                enemy.Die.AddListener(() => ActiveEnemies.Remove(enemy));
                
                EnemySpawned.Invoke(enemy);
            }

            yield return new WaitForSeconds(Random.Range(MinSpawnDelay, MaxSpawnDelay));
        }
    }

    private float GetMaxDifficulty() => 5f;

    private float GetActiveDifficulty() => ActiveEnemies
            .Sum(enemy => enemy.DifficultyUnits);

    private bool TryPickNewEnemy(out Enemy prefab)
    {
        float difficultyAvailable = GetMaxDifficulty() - GetActiveDifficulty();

        Enemy[] validEnemies = EnemyPrefabs
            .Where(enemy => enemy.CanBeSpawned)
            .Where(enemy => enemy.DifficultyUnits <= difficultyAvailable)
            .ToArray();

        if (validEnemies.Length == 0)
        {
            prefab = null;
            return false;
        }

        prefab = validEnemies.Random();
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + Vector3.up * SpawnHeight, transform.position + Vector3.down * SpawnHeight);
    }
}
