using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryMessages : MonoBehaviour
{

	float _storyUpdateEvery = 14f;
	int _currentStoryIndex = 0;

	public List<string> _storyList;

	public Text _storyText;
	public AnimationCurve _curve;
	bool canMessage = false;
	bool warned = false;

	void Start ()
	{
		StartCoroutine (ReadStory ());
	}

	void Update ()
	{
		if (PlayerStats._Health <= 20 && canMessage)
			DisplayMessage ("I can't give up!");
		if (CoreStats._Health <= 200 && canMessage)
			DisplayMessage ("It's almost destroyed! Victory is near!");
		if (!warned && PlayerStats._Warn == true)
		{
			DisplayMessage ("Yes! The ocean is helping me!");
			warned = true;
		}
	}

	IEnumerator ReadStory ()
	{
		while (_currentStoryIndex < _storyList.Count)
		{
			canMessage = false;
			DisplayMessage (_storyList[_currentStoryIndex]);
			yield return new WaitForSeconds (_storyUpdateEvery);
			_currentStoryIndex++;
		}
	}

	IEnumerator FadeIn ()
	{
		float t = 3f;

		while (t > 0f)
		{
			t -= Time.deltaTime;
			float a = _curve.Evaluate (t);
			_storyText.color = new Color (255, 255, 255, a);
			yield return 0;
		}
		canMessage = true;
	}

	IEnumerator FadeOut ()
	{
		float t = 0f;

		while (t < 2.5f)
		{
			t += Time.deltaTime;
			float a = _curve.Evaluate (t);
			_storyText.color = new Color (255, 255, 255, a);
			yield return 0;
		}
		StartCoroutine (FadeIn ());
	}

	public void DisplayMessage (string _message)
	{
		_storyText.text = _message.ToUpper ();
		StartCoroutine (FadeOut ());
	}
}