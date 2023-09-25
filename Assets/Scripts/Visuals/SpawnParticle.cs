using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticle : MonoBehaviour
{
	public float duration = 1.0f;
	public GameObject particle;

	public void Run() => StartCoroutine(Spawn());

	private IEnumerator Spawn()
	{
		var p = Instantiate(particle);
		p.transform.position = transform.position;
		yield return new WaitForSeconds(duration);
		Destroy(p);
	}
}
