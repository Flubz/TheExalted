using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class CoreStats
{
	static public int _Health { get; set; }
}

public class Core : MonoBehaviour
{

	int _initialHealth = 1000;
	Rigidbody2D _rb;
	public Image _healthBarImage;

	void Start ()
	{
		CoreStats._Health = _initialHealth;
		_rb = GetComponent<Rigidbody2D> ();
		_rb.angularVelocity = Random.Range (0.0f, 1.0f) * 60f;
		_rb.velocity = new Vector2 (Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f));
	}

	void Update ()
	{
		if (CoreStats._Health > _initialHealth)
			CoreStats._Health = _initialHealth;
			
		_healthBarImage.fillAmount = (float) CoreStats._Health / (float) _initialHealth;
	}

}