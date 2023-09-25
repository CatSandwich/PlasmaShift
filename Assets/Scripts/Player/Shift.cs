using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Shift : MonoBehaviour
{
	public float cooldown = 3;
	public float distance = 3;

	IEnumerator Start()
	{
		var circleCollider = GetComponent<CircleCollider2D>();
		var particleSpawner = GetComponent<SpawnParticle>();
		var radius = circleCollider.radius;
		var rect = new Rect(radius, radius, ResolutionHelper.cameraWorldBounds.x - radius, ResolutionHelper.cameraWorldBounds.y - radius);

		while (true)
		{
			yield return new WaitUntil(() => Input.GetMouseButtonDown(1));

			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = transform.position.z;
			Vector3 playerToMouse = (mousePos - transform.position).normalized * distance;
			Vector3 playerPos = transform.position;

			for (float i = 0; i <= 1; i += 1 / 10f)
			{
				transform.position = playerPos + playerToMouse * i;
				particleSpawner.Run();
			}

			yield return new WaitForSeconds(cooldown);
		}
	}
}
