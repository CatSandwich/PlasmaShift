using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Health : MonoBehaviour
    {
        public float CurrentHealth;
        public bool AutoDie;
        public UnityEvent Die;

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                Die.Invoke();
            }
        }

        private void Start()
        {
            if (AutoDie)
            {
                Die.AddListener(() =>
                {
                    Destroy(gameObject);
                });
            }
        }
    }
}
