using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Rebound : MonoBehaviour
{

	private void OnTriggerEnter2D (Collider2D other)
	{
		Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D> ();
		if (rb)
		{
			rb.velocity = new Vector2 (rb.velocity.x * -1, rb.velocity.y * -1);
		}
	}

	private void OnTriggerStay2D (Collider2D other)
	{
		// Debug.Log (other.name);
		if (other.gameObject.CompareTag ("Borders")) return;

		// FACEPALM

		if (other.gameObject.CompareTag ("Player"))
		{
			Player p = other.gameObject.GetComponent<Player> ();
			if (p)
				GameManager.DamagePlayer (p, (int)(500 * Time.deltaTime));
		}

		Bullet b = other.gameObject.GetComponent<Bullet> ();
		if (b)
			GameManager.DestroyGameObject (b.gameObject);

	}

}