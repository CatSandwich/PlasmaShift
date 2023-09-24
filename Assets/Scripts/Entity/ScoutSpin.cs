using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutSpin : MonoBehaviour
{
    float rotationOffset;

	private void Start()
	{
		rotationOffset = Random.value;
	}

	void Update()
    {
        float f = Mathf.Sin(Time.time * 0.2f + rotationOffset) * 90 + 45 + rotationOffset * 30;

        transform.rotation = Quaternion.Euler(f, Time.time * 30, f);
    }
}
