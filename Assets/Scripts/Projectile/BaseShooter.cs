using UnityEngine;

namespace Projectile
{
	public abstract class BaseShooter : MonoBehaviour
	{
		//Common
		protected float timer = 0;
		public float frequency = 3;
		public float speed = 4;
		public GameObject projectile;

		//Audio
		public AudioSource shooterSoundSource;
		public AudioClip[] shooterSoundClips;

		protected virtual void Update()
		{
			timer += Time.deltaTime;

			if (timer > frequency)
			{
				timer = 0;

				Shoot();
				PlaySound();
			}
		}

		protected virtual void PlaySound()
		{
			if (shooterSoundSource && shooterSoundClips.Length > 0)
			{
				shooterSoundSource.Stop();
				shooterSoundSource.clip = shooterSoundClips[Random.Range(0, shooterSoundClips.Length)];
				shooterSoundSource.PlayDelayed(Random.Range(0, Time.deltaTime)); //Prevent multiple waves from playing at the same time.
			}
		}

		protected abstract void Shoot();
	}
}
