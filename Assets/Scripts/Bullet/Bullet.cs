using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public static class BulletStats
{
	static public int _Damage { get; set; }
	static public float _Speed { get; set; }
}

public class Bullet : MonoBehaviour
{

	Rigidbody2D _rb;
	BoxCollider2D _col;
	float _initialSpeed = 16f;
	float _bulletInactivetime = 0.16f;
	public GameObject _deathParticles;

	int _initialDamage = 4;
	public int _Damage { get; set; }

	void Awake ()
	{
		_rb = GetComponent<Rigidbody2D> ();
		_col = GetComponent<BoxCollider2D> ();
	}

	void Start ()
	{
		if (BulletStats._Damage <= 0)
			BulletStats._Damage = _initialDamage;

		if (BulletStats._Speed <= 0)
			BulletStats._Speed = _initialSpeed;

		_Damage = BulletStats._Damage;

		StartCoroutine (EnableBoxCollider ());
	}

	IEnumerator CheckSpeed ()
	{
		while (true)
		{
			yield return null;
			if (_rb.velocity.magnitude < BulletStats._Speed)
				_rb.velocity = (_rb.velocity.normalized * BulletStats._Speed);
		}

	}

	public void SetVelocity (Vector2 para)
	{
		_rb.velocity = para.normalized * BulletStats._Speed;
		StartCoroutine (CheckSpeed ());
	}

	IEnumerator EnableBoxCollider ()
	{
		yield return new WaitForSeconds (_bulletInactivetime);
		_col.enabled = true;
	}

	private void OnCollisionEnter2D (Collision2D other)
	{
		switch (other.gameObject.tag)
		{
			case "Player":
				Player p = other.gameObject.GetComponent<Player> ();
				GameManager.DamagePlayer (p, _Damage);
				StartCoroutine (disableDamage ());
				break;
			case "Core":
				GameManager.DamageCore (other.gameObject.GetComponent<Core> (), (_Damage + PlayerStats._Damage));
				StartCoroutine (disableDamage ());
				break;
		}
	}

	IEnumerator disableDamage ()
	{
		PlaySound ();
		_Damage = 0;
		GameObject go = Instantiate (_deathParticles, transform.position, Quaternion.identity, transform.parent);
		GameManager.DestroyGameObject (go, 2f);
		yield return new WaitForSeconds (5f);
		_Damage = BulletStats._Damage;
	}

	void PlaySound ()
	{
		int i = Random.Range (1, 4);
		string s = "ES" + i.ToString ();
		AudioManager.instance.Play (s);
	}
}