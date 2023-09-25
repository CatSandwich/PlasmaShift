using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeShuffle : MonoBehaviour
{
    public static Vector3[] offsets =
    {
        new Vector3(-1, -1, -1),
		new Vector3(1, -1, -1),
		new Vector3(1, -1, 1),
		new Vector3(-1, -1, 1),
		new Vector3(-1, 1, 1),
		new Vector3(1, 1, 1),
		new Vector3(1, 1, -1),
		new Vector3(-1, 1, -1),
	};

	public float loopLength = 5;
	[Range(0f, 1f)]
	public float timeOffset = 0;

	public float easeInOutQuad(float x) => x < 0.5 ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;

	// Update is called once per frame
	void Update()
    {
        float time = ((timeOffset * loopLength + Time.time) % loopLength) / loopLength;

		float index = time * offsets.Length;
		float fraction = (time * offsets.Length) % 1;
		fraction = easeInOutQuad(fraction);

		int lowIndex = (int)Mathf.Floor(index);
		int highIndex = (int)Mathf.Ceil(index) % offsets.Length;
		var startPos = offsets[lowIndex];
		var endPos = offsets[highIndex];
		var offset = startPos + (endPos - startPos) * fraction;
		offset *= 0.25f;

		transform.localPosition = offset;
	}
}
