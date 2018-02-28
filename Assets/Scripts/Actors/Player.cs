using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerStats
{
	static public int _Health { get; set; }
	static public int _Damage { get; set; }
	static public float _Speed { get; set; }
	static public bool _Warn { get; set; }
}

public class Player : MonoBehaviour
{

	Rigidbody2D _playerRB;
	public Bullet _bullet;
	public Image _healthBarImage;
	public Image _hurtImage;

	int _initialHealth = 100;
	int _initialDamage = 2;
	float _initialSpeed = 8f;
	bool _movingPlayer = false;

	float _fireRate = 8.0f;
	float _timeToFire = 0.0f;
	bool _firing = false;

	public BulletContainer _bulletContainer;

	void Start ()
	{
		PlayerStats._Health = _initialHealth;
		PlayerStats._Damage = _initialDamage;
		PlayerStats._Speed = _initialSpeed;
		PlayerStats._Warn = false;

		_playerRB = gameObject.GetComponent<Rigidbody2D> ();
	}

	void Controller ()
	{
		Vector2 movement = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		_playerRB.velocity = movement * (PlayerStats._Speed / 2);
	}

	void Update ()
	{
		if (PlayerStats._Health > _initialHealth)
			PlayerStats._Health = _initialHealth;

		_healthBarImage.fillAmount = (float) PlayerStats._Health / (float) _initialHealth;

		PlayerWASDMove ();
		PlayerMouseMovement ();

		if (Time.timeScale == 1)
		{
			if (Input.GetKeyDown (KeyCode.E) || Input.GetMouseButtonDown (0))
				_firing = true;

			if (Input.GetKeyUp (KeyCode.E) || Input.GetMouseButtonUp (0))
				_firing = false;

			Vector2 ControllerShootAxis = new Vector2 (Input.GetAxisRaw ("HorizontalAxis2"), Input.GetAxisRaw ("VerticalAxis2"));
			if (Time.time > _timeToFire && _fireRate != 0)
				Shoot (ControllerShootAxis);
		}

		PlayerRotate (_playerRB.velocity);
	}

	void Shoot (Vector2 ControllerShootAxis)
	{

		if (_firing)
		{
			PlayerShootMouse ();
			_timeToFire = Time.time + 1 / _fireRate;
		}
		else if (ControllerShootAxis != Vector2.zero)
		{
			PlayerControllerShoot (ControllerShootAxis);
			_timeToFire = Time.time + 1 / _fireRate;
		}
	}

	void PlayerWASDMove ()
	{
		Vector2 movement = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		if (movement != Vector2.zero)
			_playerRB.velocity = movement.normalized * (PlayerStats._Speed);
	}

	void PlayerMouseMovement ()
	{
		if (Input.GetKeyDown (KeyCode.R) || Input.GetMouseButtonDown (1) && Time.timeScale == 1)
			_movingPlayer = true;

		if (Input.GetKeyUp (KeyCode.R) || Input.GetMouseButtonUp (1))
			_movingPlayer = false;

		if (_movingPlayer)
			PlayerMove ();
	}

	void PlayerMove ()
	{
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		Vector2 dir = mousePosition - (Vector2) transform.position;
		_playerRB.velocity = dir.normalized * PlayerStats._Speed;
	}

	void PlayerRotate (Vector3 rotAngle)
	{
		Vector3 vectorToTarget = _playerRB.velocity;
		float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * Mathf.Infinity);
	}

	void PlayerShootMouse ()
	{
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		Vector2 firePointPos = (Vector2) transform.position;
		Vector2 targetDir = mousePosition - firePointPos;

		Bullet bullet = Instantiate (_bullet, firePointPos, Quaternion.identity) as Bullet;

		bullet.SetVelocity (targetDir);
		_bulletContainer.ContainBullet (bullet);
	}

	void PlayerControllerShoot (Vector2 vec)
	{
		Vector2 firePointPos = (Vector2) transform.position;

		Bullet bullet = Instantiate (_bullet, firePointPos, Quaternion.identity) as Bullet;

		bullet.SetVelocity (vec);
		_bulletContainer.ContainBullet (bullet);
	}

	public void OnPlayerDamaged ()
	{
		StartCoroutine (PlayerHurt ());
	}

	IEnumerator PlayerHurt ()
	{
		_hurtImage.enabled = true;
		yield return new WaitForSeconds (0.1f);
		_hurtImage.enabled = false;
		yield return new WaitForSeconds (0.1f);
		_hurtImage.enabled = true;
		yield return new WaitForSeconds (0.1f);
		_hurtImage.enabled = false;
	}

}