using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoidConfig : ScriptableObject
{
    public float avoidanceWeight = 1;
    public float alignmentWeight = 1;
    public float cohesionWeight = 1;
    public float screenPadding = 4;
    public float speed = 0.3f;
}
