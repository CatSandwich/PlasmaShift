using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticle : MonoBehaviour
{
	public GameObject particle;

	public void Run()
	{
		var p = Instantiate(particle);
		p.transform.position = transform.position;
	}
}
