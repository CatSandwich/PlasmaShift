using System.Collections;
using Helpers;
using UnityEngine;

namespace Player
{
	public class Shift : MonoBehaviour
	{
		public float cooldown = 3;
		public float distance = 3;

		IEnumerator Start()
		{
			var particleSpawner = GetComponent<SpawnParticle>();
			var radius = GetComponent<CircleCollider2D>().radius;

			while (true)
			{
				yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));
			
				Vector2 direction = GetComponent<Rigidbody2D>().velocity;

				Vector2 playerPos = transform.position;
				Vector2 dest = playerPos + direction.normalized * distance;
				dest = dest.ClampToScreenBounds(radius);

				for (float i = 0; i <= 1; i += 1 / 10f)
				{
					transform.position = Vector2.Lerp(playerPos, dest, i);
					particleSpawner.Run();
				}

				yield return new WaitForSeconds(cooldown);
			}
		}
	}
}
