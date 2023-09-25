using UnityEngine;

namespace Projectile
{
	public class CircularShooter : BaseShooter
	{
		public float points = 8;

		protected override void Shoot()
		{
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
