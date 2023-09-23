using System.Collections.Generic;
using Entity;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    /// Static access to a valid object of this type.
    public static EnemyManager Instance => Singleton 
        ? Singleton 
        : Singleton = FindObjectOfType<EnemyManager>();
    private static EnemyManager Singleton;

    /// Refs to all enemy types that are in play.
    [field: SerializeField]
    public Enemy[] EnemyPrefabs { get; private set; }

    private readonly List<Enemy> ActiveEnemies = new();

    /// Spawns an instance of a given enemy prefab.
    public Enemy Spawn(Enemy prefab)
    {
        Enemy enemy = Instantiate(prefab);
        ActiveEnemies.Add(enemy);
        return enemy;
    }

    /// Kills a given enemy.
    public void Kill(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        ActiveEnemies.Remove(enemy);
    }

    /// Returns a sequence of all active enemies.
    public IEnumerable<Enemy> GetActiveEnemies()
    {
        return ActiveEnemies;
    }

    private void OnValidate()
    {
        if (EnemyPrefabs.Length == 0)
        {
            Debug.LogWarning("No enemy prefabs set. None will spawn.", this);
        }
    }
}
