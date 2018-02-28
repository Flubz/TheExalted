using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Singleton that manages the application. Primarily for scene management.
namespace Managers
{
	public class OptionsMenu : MonoBehaviour
	{
		Canvas _canvas;
		public Button _mainMenuButton;

		void Start ()
		{
			_canvas = GetComponent<Canvas> ();
		}

		public void MenuOn ()
		{
			transform.position = Camera.main.transform.position;
			if (_canvas) _canvas.enabled = true;
			if (SceneManager.GetActiveScene ().name == "Level01")
				_mainMenuButton.interactable = true;
		}

		public void MenuOff ()
		{
			if (_canvas) _canvas.enabled = false;
			_mainMenuButton.interactable = false;
		}

		public void TogglePP ()
		{
			PostProcessingBehaviour p = Camera.main.GetComponent<PostProcessingBehaviour> ();
			if (p) p.enabled = !(p.enabled);
		}
	}
}