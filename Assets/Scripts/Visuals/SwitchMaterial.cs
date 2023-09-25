using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMaterial : MonoBehaviour
{
	public void SwitchTo(Material material)
	{
		GetComponent<Renderer>().material = material;
	}
}
