using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Health : MonoBehaviour
    {
        public float CurrentHealth;
        public bool AutoDie;
        public UnityEvent Die;

        //Audio
        public AudioSource damageSoundSource;
        public AudioClip[] damageSoundClips;
        public float damagePitchMin;
        public float damagePitchMax;


        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;

            if (damageSoundSource != null && damageSoundClips.Length > 0)
			{
                damageSoundSource.Stop(); //possibly redundant?
                damageSoundSource.clip = damageSoundClips[Random.Range(0, damageSoundClips.Length)];
                damageSoundSource.pitch = Random.Range(damagePitchMin, damagePitchMax);
                damageSoundSource.Play();
			}

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
