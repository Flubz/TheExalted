using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContainer : MonoBehaviour
{
	List<Bullet> _bulletList;

	void Awake ()
	{
		BulletStats._Damage = 0;
	}

	void Start ()
	{
		_bulletList = new List<Bullet> ();
	}

	public void ContainBullet (Bullet bullet)
	{
		bullet.transform.parent = gameObject.transform;
		_bulletList.Add (bullet);
	}
}