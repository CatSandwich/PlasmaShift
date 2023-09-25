using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMaterialEdit : MonoBehaviour
{
	public Material material;

	public void SetFloat( string key, float value )
	{
		material.SetFloat( key, value );
	}
}
