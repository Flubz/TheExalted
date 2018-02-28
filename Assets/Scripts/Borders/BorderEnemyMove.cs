using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderEnemyMove : MonoBehaviour
{
	public float yVel = 0f;
	public float xVel = 0f;
	public static float _moveSpeed = 0.06f;
	Rigidbody2D _rb;
	void Start ()
	{
		_rb = GetComponent<Rigidbody2D> ();
		if (_rb)
			_rb.velocity = new Vector2 (xVel * _moveSpeed, yVel * _moveSpeed);
	}

	public Vector2 GetBEMVec ()
	{
		return new Vector2 (xVel, yVel);
	}
}