using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularShooter : MonoBehaviour
{
	float timer = 0;
	public float frequency = 3;
	public float speed = 4;
	public float points = 8;
	public GameObject projectile;

	void Update()
	{
		timer += Time.deltaTime;

		if (timer > frequency)
		{
			timer = 0;

			for (int a = 0; a < points; a++)
			{
				float angle = (a * Mathf.PI * 2) / points;
				var proj = Instantiate(projectile);
				var rb = proj.GetComponent<Rigidbody2D>();
				rb.velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
				proj.transform.position = transform.position;
			}
		}
	}
}
