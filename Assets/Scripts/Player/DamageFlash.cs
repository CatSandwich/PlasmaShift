using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class DamageFlash : MonoBehaviour
	{
		public float damageFlashDuration = 0.5f;
		SpriteRenderer spriteRenderer;
		Coroutine coroutine;

		public void Run()
		{
			if (coroutine != null)
			{
				StopCoroutine(coroutine);
			}
			coroutine = StartCoroutine(DoFlash());
		}

		IEnumerator DoFlash()
		{
			float destTime = Time.time + damageFlashDuration;

			while (Time.time < destTime)
			{
				yield return null;
				float progress = 1 - (destTime - Time.time) / damageFlashDuration;
				spriteRenderer.color = Color.Lerp(Color.red, Color.white, progress);
			}

			spriteRenderer.color = Color.white;
		}

		void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}
	}

}