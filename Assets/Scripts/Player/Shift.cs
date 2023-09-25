using System.Collections;
using UnityEngine;
using Helpers;

public class Shift : MonoBehaviour
{
	public float cooldown = 3;
	public float distance = 3;

	IEnumerator Start()
	{
		float radius = GetComponent<CircleCollider2D>().radius;

		while (true)
		{
			yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));

			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = transform.position.z;
			
			Vector3 playerToMouse = mousePos - transform.position;
			float clampedDistance = Mathf.Min(distance, playerToMouse.magnitude);

			transform.position += playerToMouse.normalized * clampedDistance;
			transform.position = transform.position.ClampToScreenBounds(radius);

			yield return new WaitForSeconds(cooldown);
		}
	}
}
