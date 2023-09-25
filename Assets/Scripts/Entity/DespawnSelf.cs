using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnSelf : MonoBehaviour
{
	public float duration = 1.0f;

	IEnumerator Start()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
