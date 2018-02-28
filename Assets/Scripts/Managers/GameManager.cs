using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
	public class GameManager : MonoBehaviour
	{

		public static GameManager _gameManagerInstance;
		static ApplicationManager _applicationManager;
		public static int _Score { get; set; }

		void Awake ()
		{
			if (_gameManagerInstance == null)
				_gameManagerInstance = this;
			else if (_gameManagerInstance != this)
				Destroy (gameObject);

		}

		void Start ()
		{
			_applicationManager = ApplicationManager._applicationManagerInstance;

			if (_applicationManager == null)
				_applicationManager = GameObject.FindGameObjectWithTag ("ApplicatioManager").GetComponent<ApplicationManager> ();

			Time.timeScale = 1f;
			ResetStatics ();
		}

		void Update ()
		{
			if (Input.GetKeyDown (KeyCode.Escape) || Input.GetButtonDown ("Cancel"))
				_applicationManager.OptionsMenuOn ();
		}

		public static void ResetStatics ()
		{
			_Score = 0;
		}

		public static void KillPlayer (Player _player, float delay)
		{
			_applicationManager.VicMenuOn (false);
			Destroy (_player.gameObject, delay);
		}

		public static void KillCore (Core _core, float delay)
		{
			_applicationManager.VicMenuOn (true);
			Destroy (_core.gameObject, delay);
		}

		public static void DamagePlayer (Player _player, int _damage = 0)
		{
			_player.OnPlayerDamaged ();
			PlayerStats._Health -= _damage;
			if (PlayerStats._Health <= 0)
				KillPlayer (_player, 0.0f);
		}

		public static void DamageCore (Core _core, int _damage = 0)
		{
			CoreStats._Health -= _damage;
			if (CoreStats._Health <= 0)
				KillCore (_core, 0.0f);
		}

		public static void DestroyGameObject (GameObject go, float delay = 0)
		{
			Destroy (go, delay);
		}

	}
}