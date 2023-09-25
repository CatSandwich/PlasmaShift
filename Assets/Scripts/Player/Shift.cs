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
