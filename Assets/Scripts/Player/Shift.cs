using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Shift : MonoBehaviour
{
	public float cooldown = 3;
	public float distance = 3;
	CircleCollider2D circleCollider;

	IEnumerator Start()
	{
		circleCollider = GetComponent<CircleCollider2D>();
		var radius = circleCollider.radius;
		var rect = new Rect(radius, radius, ResolutionHelper.cameraWorldBounds.x - radius, ResolutionHelper.cameraWorldBounds.y - radius);

		while (true)
		{
			yield return new WaitUntil(() => Input.GetMouseButtonDown(1));

			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = transform.position.z;
			Vector3 playerToMouse = (mousePos - transform.position).normalized;

			transform.position = BoundsHelper.ClampToScreenBounds(transform.position + playerToMouse * distance, radius);

			yield return new WaitForSeconds(cooldown);
		}
	}
}
