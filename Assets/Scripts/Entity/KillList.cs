using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillList : MonoBehaviour
{
	public List<GameObject> objects = new List<GameObject>();

	public void Kill()
	{
		if (objects.Count == 0) return;
		var obj = objects[0];
		objects.RemoveAt(0);
		Destroy(obj);
	}
}
