using System.Collections;
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

        public AudioClip deathClip;
        private float DEATH_VOLUME = 0.5f;


        public void TakeDamage(float damage)
        {
            // If already dead, ignore further damage.
            if (CurrentHealth <= 0)
            {
                return;
            }
            
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
                    if (deathClip != null)
                    {
                        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position, DEATH_VOLUME);
                    }
                    Destroy(gameObject);
                });
            }
			else
			{
                Die.AddListener(() =>
                {
                    if (deathClip != null) 
                    { 
                        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position, DEATH_VOLUME); 
                    }
                });
            }
        }
    }
}
