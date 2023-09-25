using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static float shakeTime = 0;
    public static bool isShaking = false;
    public float shakeDuration = 0.2f;
    public float shakeIntensity = 0.01f;

    public Vector3 camPos;

	private void Start()
	{
		camPos = transform.position;
	}

    public static void StartShake()
    {
        shakeTime = 0;
        isShaking = true;
    }

	void Update()
    {
        if (isShaking)
        {
            if (shakeTime > shakeDuration)
            {
				transform.position = camPos;
				shakeTime = 0;
                isShaking = false;
            }
            else
            {
                float fraction = 1 - (shakeTime / shakeDuration);
                transform.position += new Vector3(
                    (Random.value > 0.5 ? -shakeIntensity : shakeIntensity) * fraction,
					(Random.value > 0.5 ? -shakeIntensity : shakeIntensity) * fraction,
					0
                    );
			}
        }

        shakeTime += Time.deltaTime;
    }
}
