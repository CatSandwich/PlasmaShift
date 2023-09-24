using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Entity;
using Helpers;
using States;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public float MinSpawnDelay;
    public float MaxSpawnDelay;
    
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
                Enemy enemy = Instantiate(prefab, transform.position, Quaternion.identity, transform);
                
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
}
