using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (SpriteRenderer))]
[RequireComponent (typeof (Rigidbody2D))]
public class Upgrade : MonoBehaviour
{
	[SerializeField]
	protected string _messageToDisplay;
	protected bool _pickUpStatus = false;
	[SerializeField]
	protected GameObject _pickUpEffect;
	[SerializeField]
	protected AnimationCurve _curve;
	public Text _upgradeText;

	bool _alreadyDead;

	SpriteRenderer _sr;

	void Start ()
	{
		_sr = GetComponent<SpriteRenderer> ();
		_upgradeText = GameObject.FindGameObjectWithTag ("UpgradeText").GetComponent<Text> ();
	}

	public void PickUpEffect ()
	{
		if (_alreadyDead) return;

		_alreadyDead = true;
		GameObject go = Instantiate (_pickUpEffect, transform.position, Quaternion.identity);
		StartCoroutine (FadeOutSR ());
		GameManager.DestroyGameObject (go, 2f);
	}

	IEnumerator FadeOutSR ()
	{
		float t = 0.5f;

		while (t > 0f)
		{
			t -= Time.deltaTime;
			float a = _curve.Evaluate (t);
			_sr.color = new Color (_sr.color.r, _sr.color.g, _sr.color.b, a);
			yield return 0;
		}
	}

	IEnumerator FadeIn ()
	{
		float t = 1.5f;

		while (t > 0f)
		{
			t -= Time.deltaTime;
			float a = _curve.Evaluate (t);
			_upgradeText.color = new Color (255, 255, 255, a);
			yield return 0;
		}
	}

	IEnumerator FadeOut ()
	{
		float t = 0f;

		while (t < 0.5f)
		{
			t += Time.deltaTime;
			float a = _curve.Evaluate (t);
			_upgradeText.color = new Color (255, 255, 255, a);
			yield return 0;
		}
		StartCoroutine (FadeIn ());
	}

	public void DisplayMessage (string _message)
	{
		_upgradeText.text = _message;
		StartCoroutine (FadeOut ());
		PickUpEffect ();
	}

}