using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

// Singleton that manages the application. Primarily for scene management.
namespace Managers
{
	public class MainMenu : MonoBehaviour
	{

		ApplicationManager _applicationManager;
		public List<string> _hintsList;
		public Text _textField;

		void Start ()
		{
			_applicationManager = ApplicationManager._applicationManagerInstance;
			RandomHintMessage ();
		}

		ApplicationManager GetApplicationManager ()
		{
			if (_applicationManager != null)
				return _applicationManager;
			else
				Debug.LogError ("Application Manager is NULL!");
			return null;
		}

		void RandomHintMessage ()
		{
			int x = Random.Range(0, _hintsList.Count);
			_textField.text = _hintsList[x];
		}

		public void OnClickLevel01 ()
		{
			GetApplicationManager ().LoadLevel01 ();
		}

		public void OnClickOptions ()
		{
			GetApplicationManager ().OptionsMenuOn ();
		}

		public void OnClickExit ()
		{
			GetApplicationManager ().LoadEnd ();
		}

	}
}