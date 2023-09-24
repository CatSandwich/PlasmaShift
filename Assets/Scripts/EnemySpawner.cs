using System.Collections;
using System.Linq;
using Entity;
using Helpers;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyManager EnemyManager;

    public float MinSpawnDelay;
    public float MaxSpawnDelay;

    private IEnumerator Start()
    {
        while (true)
        {
            if (TryPickNewEnemy(out Enemy prefab))
            {
                Enemy enemy = EnemyManager.Spawn(prefab);
                enemy.transform.position = transform.position;
                enemy.transform.SetParent(transform, true);
            }

            yield return new WaitForSeconds(Random.Range(MinSpawnDelay, MaxSpawnDelay));
        }
    }

    private float GetMaxDifficulty() => 5f;

    private float GetActiveDifficulty() => EnemyManager
            .GetActiveEnemies()
            .Sum(enemy => enemy.DifficultyUnits);

    private bool TryPickNewEnemy(out Enemy prefab)
    {
        float difficultyAvailable = GetMaxDifficulty() - GetActiveDifficulty();

        Enemy[] validEnemies = EnemyManager
            .EnemyPrefabs
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
