using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public GameObject boid;

    static float boidSpread = 1.3f;

    void Start()
    {
        for (float x = 0; x < ResolutionHelper.cameraWorldBounds.x; x += boidSpread)
        {
            for (float y = 0; y < ResolutionHelper.cameraWorldBounds.y; y += boidSpread)
            {
                var newBoid = Instantiate(boid, transform);
                newBoid.transform.position = new Vector3(
                    x + Random.Range(-0.1f, 0.1f),
                    y + Random.Range(-0.1f, 0.1f), 
                    0);
            }
        }
    }
}
