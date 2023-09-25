using UnityEngine;

namespace Projectile
{
	public class SimpleShooter : BaseShooter
	{
		GameObject player;

		private void Start()
		{
			player = GameObject.FindWithTag("Player");
		}
		
		protected override void Shoot()
		{
			if (!player) return;
			
			timer = 0;
			var proj = Instantiate(projectile);
			var rb = proj.GetComponent<Rigidbody2D>();
			rb.velocity = (player.transform.position - transform.position).normalized * speed;
			proj.transform.position = transform.position;
		}
	}
}
