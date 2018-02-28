using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

// Singleton that manages the application. Primarily for scene management.
namespace Managers
{

	public class ApplicationManager : MonoBehaviour
	{

		public static ApplicationManager _applicationManagerInstance = null;
		static OptionsMenu _optionsMenu;
		static VictoryMenu _vicMenu;
		static SceneFader _sceneFader;
		static AudioManager _audioManager;

		void Awake ()
		{
			if (_applicationManagerInstance == null)
				_applicationManagerInstance = this;
			else if (_applicationManagerInstance != this)
				Destroy (gameObject);


			DontDestroyOnLoad (gameObject);
		}

		void Start ()
		{
			_audioManager = AudioManager.instance;
			_audioManager.Play("ExaltMain");
			
			_optionsMenu = GetComponentInChildren<OptionsMenu> ();
			_optionsMenu.MenuOff ();
			_vicMenu = GetComponentInChildren<VictoryMenu> ();
			_vicMenu.MenuOff ();
			_sceneFader = GetComponentInChildren<SceneFader> ();

			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		void OnSceneLoaded (Scene _scene, LoadSceneMode _mode)
		{
			_sceneFader.FadeFrom ();

			Time.timeScale = 1;
			switchMenusOff ();
		}

		public void LoadMainMenu ()
		{
			Time.timeScale = 1;
			_sceneFader.FadeTo ("MainMenu");
		}

		public void LoadEnd ()
		{
			Application.Quit ();
		}

		public void LoadLevel01 ()
		{
			_sceneFader.FadeTo ("Level01");
		}

		public void OptionsMenuOn ()
		{
			if (_optionsMenu) _optionsMenu.MenuOn ();
			Time.timeScale = 0;
		}

		public void OptionsMenuOff ()
		{
			if (_optionsMenu) _optionsMenu.MenuOff ();
			Time.timeScale = 1;
		}

		public void VicMenuOn (bool vicState)
		{
			Time.timeScale = 0;
			if (_vicMenu) _vicMenu.MenuOn (vicState);
		}

		public void VicMenuOff ()
		{
			if (_vicMenu) _vicMenu.MenuOff ();
			Time.timeScale = 1;
		}

		public void switchMenusOff ()
		{
			VicMenuOff ();
			OptionsMenuOff ();
		}
	}
}