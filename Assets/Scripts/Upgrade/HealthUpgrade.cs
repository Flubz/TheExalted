using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : Upgrade
{
	public int _increaseHealthAmount = 40;

	private void OnCollisionEnter2D (Collision2D other)
	{
		if (!_pickUpStatus && other.gameObject.CompareTag ("Player"))
		{
			PlayerStats._Health += _increaseHealthAmount;
			CoreStats._Health += _increaseHealthAmount;
			PlayerStats._Warn = true;
			_pickUpStatus = true;
			DisplayMessage(_messageToDisplay);
		}
	}
}