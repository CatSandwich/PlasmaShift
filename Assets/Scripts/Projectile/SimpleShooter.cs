using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShooter : MonoBehaviour
{
    float timer = 0;
    public float frequency = 3;
    public float speed = 4;
    public GameObject projectile;
    GameObject player;

	private void Start()
	{
        player = GameObject.FindWithTag("Player");
	}

	void Update()
    {
        if (!player) return;

        timer += Time.deltaTime;

        if (timer > frequency)
        {
            timer = 0;
            var proj = Instantiate(projectile);
            var rb = proj.GetComponent<Rigidbody2D>();
            rb.velocity = (player.transform.position - transform.position).normalized * speed;
            proj.transform.position = transform.position;
        }
    }
}
