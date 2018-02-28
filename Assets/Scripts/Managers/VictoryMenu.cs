using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

// Singleton that manages the application. Primarily for scene management.
namespace Managers
{

	public class VictoryMenu : MonoBehaviour
	{
		Canvas _canvas;
		CanvasGroup _canvasGroup;
		public Text _vicText;
		public Text _flavourText;
		public AnimationCurve _curve;

		void Start ()
		{
			_canvas = GetComponent<Canvas> ();
			_canvasGroup = GetComponent<CanvasGroup> ();
		}

		IEnumerator FadeIn ()
		{
			float t = 3f;

			while (t > 0f)
			{
				t -= Time.unscaledDeltaTime;
				float a = _curve.Evaluate (t);
				_canvasGroup.alpha = a;
				yield return 0;
			}
		}

		IEnumerator FadeOut ()
		{
			float t = 0f;

			while (t < 3.5f)
			{
				t += Time.unscaledDeltaTime;
				float a = _curve.Evaluate (t);
				_canvasGroup.alpha = a;
				yield return 0;
			}
		}

		public void MenuOn (bool vicState)
		{
			if (_canvas) _canvas.enabled = true;

			StartCoroutine (FadeOut ());

			if (vicState)
			{
				_vicText.text = "VICTORY!";
				_flavourText.text = "THE EXALTED TOOK PRIDE IN HER VICTORY THIS DAY!";
			}
			else
			{
				_vicText.text = "THE EXALTED HAS FALLEN!";
				_flavourText.text = "DEATH BY YOUR OWN WEAPONS! MAKE EVERY SHOT COUNT!";
			}
		}

		public void MenuOff ()
		{
			if (_canvas) _canvas.enabled = false;
		}
	}
}