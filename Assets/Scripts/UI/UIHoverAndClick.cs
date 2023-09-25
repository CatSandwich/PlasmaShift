using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverAndClick : MonoBehaviour, IPointerEnterHandler
{
	public AudioSource UIEnterSource;
	public AudioClip UIEnterSound;
	public AudioClip UIConfirmSound;
	public const float UISoundVolume = 0.3f;


	private void Start()
	{
		UIEnterSource.volume = UISoundVolume;
		UIEnterSource.clip = UIEnterSound;
	}

	public void OnConfirm()
	{
		if (!UIConfirmSound) return;
		AudioSource.PlayClipAtPoint(UIConfirmSound, Camera.main.transform.position);
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!UIEnterSound && !UIEnterSource) return;

		UIEnterSource.Stop(); //Redundant?
		UIEnterSource.PlayScheduled(AudioSettings.dspTime);
	}
}
