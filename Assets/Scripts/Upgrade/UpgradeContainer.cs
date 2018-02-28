using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeContainer : MonoBehaviour
{
	private float _upgradeSpawnTime = 10f;
	private float _upgradeLaunchSpeed = 6f;
	public List<GameObject> _upgradeList;

	List<Upgrade> _upgradesContainer;

	void Start ()
	{
		_upgradesContainer = new List<Upgrade> ();
		StartCoroutine (SpawnUpgrades ());
	}

	IEnumerator SpawnUpgrades ()
	{
		while (true)
		{
			yield return new WaitForSeconds (_upgradeSpawnTime);
			int i = Random.Range (0, _upgradeList.Count);
			GameObject g = Instantiate (_upgradeList[i], transform.position, Quaternion.identity, transform);
			g.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range (0.5f, 1.0f) * _upgradeLaunchSpeed, Random.Range (0.5f, 1.0f) * _upgradeLaunchSpeed);
			_upgradesContainer.Add (g.GetComponent<Upgrade> ());
		}
	}

}