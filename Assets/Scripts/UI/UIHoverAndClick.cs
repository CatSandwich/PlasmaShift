using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverAndClick : MonoBehaviour, IPointerEnterHandler
{
	public AudioClip UIEnterSound;
	public AudioClip UIConfirmSound;
	public const float UISoundVolume = 0.5f;

	public void OnConfirm()
	{
		if (!UIConfirmSound) return;
		AudioSource.PlayClipAtPoint(UIConfirmSound, Camera.main.transform.position);
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!UIEnterSound) return;
		AudioSource.PlayClipAtPoint(UIEnterSound, Camera.main.transform.position);
	}
}
