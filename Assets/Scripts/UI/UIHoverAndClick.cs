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
		AudioSource.PlayClipAtPoint(UIConfirmSound, Camera.main.transform.position);
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		AudioSource.PlayClipAtPoint(UIEnterSound, Camera.main.transform.position);
	}
}
