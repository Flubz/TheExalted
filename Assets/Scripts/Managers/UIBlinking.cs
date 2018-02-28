using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlinking : MonoBehaviour
{

	Image[] _sr;

	[Range (0, 1)]
	public float _minAlpha = 0.2f;

	[Range (0, 1)]
	public float _maxAlpha = 0.26f;

	void Start ()
	{
		_sr = GetComponentsInChildren<Image> ();
		StartCoroutine (Blink ());
	}

	IEnumerator Blink ()
	{
		while (true)
		{
			float x = Random.Range (_minAlpha, _maxAlpha);
			foreach (Image sr in _sr)
			{
				Color c = sr.color;
				sr.color = new Color (c.r, c.g, c.b, x);
			}

			yield return new WaitForSecondsRealtime (0.1f);
		}
	}

}