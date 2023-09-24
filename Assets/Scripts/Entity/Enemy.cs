using UnityEngine;
using UnityEngine.Events;

namespace Entity
{
    public class Enemy : MonoBehaviour
    {
        /// Denotes how much difficulty this enemy provides while active.
        [field: SerializeField]
        public virtual float DifficultyUnits { get; protected set; } = 1f;

        /// Denotes if this enemy can be spawned at any given time.
        public virtual bool CanBeSpawned { get; protected set; } = true;

        /// Invoked when this enemy dies.
        public UnityEvent Die;

        /// Invokes <see cref="Die"/>.
        public void InvokeDie()
        {
            Die.Invoke();
        }
    }
}