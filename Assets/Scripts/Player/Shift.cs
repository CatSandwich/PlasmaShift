using System.Collections;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
	public class Shift : MonoBehaviour
	{
		public AudioSource shiftSource;
		public AudioClip[] shiftClips;
		public AudioSource shiftRechargeSource;
		public AudioClip[] shiftRechargeClips;

		private const double RECHARGE_SOUND_MARKER = 0.99571428571d;

		private Rigidbody2D Rigidbody;
		private SpawnParticle ParticleSpawner;
		private float Radius;
		
		public float cooldown = 3;
		public float distance = 3;

		IEnumerator Start()
		{
			Rigidbody = GetComponent<Rigidbody2D>();
			ParticleSpawner = GetComponent<SpawnParticle>();
			Radius = GetComponent<CircleCollider2D>().radius;

			while (true)
			{
				yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));
				ShiftAudio();
				ShiftImpl();
				yield return DoProgress();
			}
		}

		private void ShiftAudio()
		{
			if (shiftSource && shiftClips.Length > 0)
			{
				shiftSource.Stop(); //Redundant?
				shiftSource.clip = shiftClips[Random.Range(0, shiftClips.Length)];
				shiftSource.Play();
			}
			if (shiftRechargeSource && shiftRechargeClips.Length > 0)
			{
				shiftRechargeSource.Stop(); //Redundant?
				shiftRechargeSource.clip = shiftRechargeClips[Random.Range(0, shiftRechargeClips.Length)];
				shiftRechargeSource.PlayScheduled(AudioSettings.dspTime + (cooldown - RECHARGE_SOUND_MARKER));
			}
		}

		private void ShiftImpl()
		{
			Vector2 direction = Rigidbody.velocity;
			Vector2 start = transform.position;
			Vector2 dest = (start + direction.normalized * distance).ClampToScreenBounds(Radius);

			for (float i = 0; i <= 1; i += 1 / 10f)
			{
				transform.position = Vector2.Lerp(start, dest, i);
				ParticleSpawner.Run();
			}
		}

		public Image progressImage;

		IEnumerator DoProgress()
		{
			float destTime = Time.time + cooldown;

			progressImage.gameObject.SetActive(true);

			while (Time.time < destTime)
			{
				yield return null;
				float progress = (destTime - Time.time) / cooldown;
				progressImage.fillAmount = progress;
			}

			progressImage.gameObject.SetActive(false);
		}
	}
}
