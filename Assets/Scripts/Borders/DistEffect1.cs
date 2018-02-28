using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistEffect1 : MonoBehaviour
{

	public Transform _distEffect;
	public int _numberOfEffect;
	public int _offsets;

	void Start ()
	{
		for (int i = 0; i < _numberOfEffect; i++)
		{
			Transform t = Instantiate (_distEffect, transform.position + ((Vector3.down) * _offsets * i), Quaternion.identity, transform) as Transform;
			t.rotation = Quaternion.Euler (0, 0, Random.Range (0.0f, 1.0f));
			t.localScale = (Vector3.one * Random.Range (0.8f, 2.0f));
			SpriteRenderer sr = t.GetComponent<SpriteRenderer>();
			sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, Random.Range(0.4f, 1f));
		}
	}

}