using Damage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
	RectTransform rectTransform;
	float width;

	public GameObject progressBar;
	RectTransform progressBarRectTransform;
	Image progressImage;

	Player.Health health;
	float maxHealth;

	Color red;
	Color white;

	void Start()
	{
		var player = GameObject.FindWithTag("Player");
		health = player.GetComponent<Player.Health>();
		maxHealth = health.CurrentHealth;
		player.GetComponent<DamageReceiver>().DamageReceived.AddListener(UpdateHealth);
		progressImage = progressBar.GetComponent<Image>();

		rectTransform = GetComponent<RectTransform>();
		progressBarRectTransform = progressBar.GetComponent<RectTransform>();
		width = Screen.width + rectTransform.sizeDelta.x;

		red = new Color(1, 0, 0, progressImage.color.a);
		white = new Color(1, 1, 1, progressImage.color.a);
	}

	float currentProgress = 0;
	public float approachSpeed = 0.001f;
	public float approachTime = 0.2f;

	Coroutine coroutine;
	IEnumerator Approach(float t)
	{
		float destTime = Time.time + approachTime;

		while (Time.time < destTime)
		{
			yield return null;
			currentProgress = Mathf.Lerp(currentProgress, t, approachSpeed * Time.deltaTime);
			SetProgress(currentProgress);

			float progress = 1 - (destTime - Time.time) / approachTime;
			progressImage.color = Color.Lerp(red, white, progress);
		}

		progressImage.color = white;
		currentProgress = t;
		SetProgress(t);
	}

	void SetProgress(float t)
	{
		RectTransformExtensions.SetRight(progressBarRectTransform, width * t);
	}

	void UpdateHealth(float _)
	{
		float t = 1 - health.CurrentHealth / maxHealth;

		if (coroutine != null)
		{
			StopCoroutine(coroutine);
		}
		coroutine = StartCoroutine(Approach(t));
	}
}
