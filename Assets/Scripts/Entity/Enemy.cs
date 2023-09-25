using UnityEngine;
using UnityEngine.Events;

namespace Entity
{
    public class Enemy : MonoBehaviour
    {
        /// Denotes how much difficulty this enemy provides while active.
        [field: SerializeField]
        public virtual float DifficultyUnits { get; protected set; } = 1f;

        /// How much score the player earns for killing this enemy.
        [field: SerializeField]
        public virtual int Score { get; set; } = 50;

        /// Denotes if this enemy can be spawned at any given time.
        public virtual bool CanBeSpawned { get; protected set; } = true;

        /// Invoked when this enemy dies.
        public UnityEvent Die;

        private void OnDestroy()
        {
            Die.Invoke();
        }
    }
}