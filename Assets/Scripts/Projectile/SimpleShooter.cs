using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShooter : BaseShooter
{
    GameObject player;

	private void Start()
	{
        player = GameObject.FindWithTag("Player");
	}

	protected override void Update()
    {
        if (!player) return;
        base.Update();
    }


	protected override void Shoot()
	{
        timer = 0;
        var proj = Instantiate(projectile);
        var rb = proj.GetComponent<Rigidbody2D>();
        rb.velocity = (player.transform.position - transform.position).normalized * speed;
        proj.transform.position = transform.position;
    }
}
