using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade : Upgrade
{
	public int _damageIncreaseAmount = 5;

	private void OnCollisionEnter2D (Collision2D other)
	{
		if (!_pickUpStatus && other.gameObject.CompareTag ("Player"))
		{
			BulletStats._Damage += _damageIncreaseAmount;
			PlayerStats._Warn = true;
			_pickUpStatus = true;
			DisplayMessage (_messageToDisplay);
		}
	}
}