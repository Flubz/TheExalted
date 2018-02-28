using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : Upgrade
{
	public float _increaseSpeedAmount = 2;

	private void OnCollisionEnter2D (Collision2D other)
	{
		if (!_pickUpStatus && other.gameObject.CompareTag ("Player"))
		{
			PlayerStats._Speed += _increaseSpeedAmount;
			BulletStats._Speed += _increaseSpeedAmount;
			PlayerStats._Warn = true;
			_pickUpStatus = true;
			DisplayMessage (_messageToDisplay);
		}
	}
}