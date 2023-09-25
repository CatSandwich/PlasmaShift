using System.Collections;
using Helpers;
using UnityEngine;

namespace Player
{
	public class Shift : MonoBehaviour
	{
		public AudioSource shiftSource;
		public AudioClip[] shiftClips;
		public AudioSource shiftRechargeSource;
		public AudioClip[] shiftRechargeClips;

		private const double RECHARGE_SOUND_MARKER = 0.99571428571d;

		public float cooldown = 3;
		public float distance = 3;

		IEnumerator Start()
		{
			var particleSpawner = GetComponent<SpawnParticle>();
			var radius = GetComponent<CircleCollider2D>().radius;

			while (true)
			{
				yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));

				//Do audio
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

				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mousePos.z = transform.position.z;
			
				Vector3 playerToMouse = mousePos - transform.position;
				float clampedDistance = Mathf.Min(distance, playerToMouse.magnitude);
			
				Vector3 playerPos = transform.position;
				Vector3 dest = playerPos + playerToMouse.normalized * clampedDistance;
				dest = dest.ClampToScreenBounds(radius);

				for (float i = 0; i <= 1; i += 1 / 10f)
				{
					transform.position = Vector3.MoveTowards(playerPos, dest, clampedDistance * i);
					particleSpawner.Run();
				}

				yield return new WaitForSeconds(cooldown);
			}
		}
	}
}
