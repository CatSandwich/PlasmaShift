using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
	public Rigidbody2D rb;
    public Collider2D sphereCollider;
    public List<Boid> localBoids = new List<Boid>();
    public BoidConfig config;

    private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sphereCollider = rb.GetComponent<Collider2D>();

		float randAngle = Random.Range(0, Mathf.PI * 2);
		rb.velocity = new Vector2(Mathf.Cos(randAngle), Mathf.Sin(randAngle));
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Boid>(out var boid))
		{
            localBoids.Add(boid);
        }
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.TryGetComponent<Boid>(out var boid))
        {
            localBoids.Remove(boid);
        }
    }

	private void Update()
	{
		if (
			transform.position.x < -config.screenPadding ||
			transform.position.y < -config.screenPadding ||
			transform.position.x > ResolutionHelper.cameraWorldBounds.x + config.screenPadding ||
            transform.position.y > ResolutionHelper.cameraWorldBounds.y + config.screenPadding
            )
		{
            rb.velocity = -(new Vector2(transform.position.x, transform.position.y) - ResolutionHelper.cameraWorldBounds / 2).normalized * config.speed;
		}

		else
		{
            if (localBoids.Count > 0)
            {
                var avgAvoidance = new Vector3();
                var avgDirection = new Vector2();
                var avgPosition = new Vector3();

                int avoided = 0;

                foreach (Boid boid in localBoids)
                {
                    var avoidanceVec = transform.position - boid.transform.position;
                    if (avoidanceVec.magnitude <= 0.8)
                    {
                        avgAvoidance += avoidanceVec;
                        avoided++;
                    }

                    avgDirection += boid.rb.velocity.normalized;
                    avgPosition += boid.transform.position;
                }

                avgAvoidance /= avoided;
                avgDirection /= localBoids.Count;
                avgPosition /= localBoids.Count;

                // float closeness = Mathf.Max(0, 1 - avgAvoidance.magnitude);
                rb.velocity = Vector3.Slerp(rb.velocity, avgAvoidance.normalized, config.avoidanceWeight * Time.deltaTime);
                rb.velocity = Vector3.Slerp(rb.velocity, avgDirection.normalized, config.alignmentWeight * Time.deltaTime);
                rb.velocity = Vector3.Slerp(rb.velocity, (avgPosition - transform.position).normalized, config.cohesionWeight * Time.deltaTime);
                rb.velocity = rb.velocity.normalized * config.speed;
            }
        }
	}
}
