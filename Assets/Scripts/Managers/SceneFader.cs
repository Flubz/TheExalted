using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// From Brackeys. Reasoning: Having the scene transition just looks nicer. 
// I edited the code some and added to namespace.
// I can claim to understand the code, see comments.
// I've added loading functionality to the script as well.
// Made it look nicer in the inspector.
namespace Managers
{
    public class SceneFader : MonoBehaviour
    {
        
        [Header("Scene Fader")]
        public Image _image;
        public AnimationCurve _curve;
        public float _fadeDuration = 1.5f;

        [Space(2)]

        [Header("Loading")]
        public GameObject _loadingPanel;
        public Image _loadingAmount;

        void Start ()
        {
            _loadingPanel.SetActive(false);                        
            StartCoroutine (FadeIn ());
        }

        public void FadeFrom ()
        {
            StartCoroutine (FadeIn());
        }

        public void FadeTo (string scene)
        {
            StartCoroutine (FadeOut (scene));
        }

        IEnumerator FadeIn ()
        {
            float t = _fadeDuration;            

            while (t > 0f)
            {
                t -= Time.deltaTime;      
                // Decreases t by frame time.
                float a = _curve.Evaluate (t);
                // a is evaluating t and extrapolating it depending upon the _curve height.
                // Instead of lerping or doing it linearly. 
                // In this case I'm just smoothing it out through the inspector.
                _image.color = new Color (200f, 255f, 255f, a);
                // a in alpha to fade the image.
                yield return 0;
                // No wait time.
                // Fade out is similar but switches to another scene on end.
            }
        }

        IEnumerator FadeOut (string scene)
        {
            float t = 0f;

            while (t < _fadeDuration)
            {
                t += Time.deltaTime;
                float a = _curve.Evaluate (t);
                _image.color = new Color (200f, 255f, 255f, a);
                yield return 0;
            }

            StartCoroutine(LoadAsync(scene));
        }

        // Also from Brackeys. Implemented myself; edited some.
        IEnumerator LoadAsync (string scene)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync (scene);

            _loadingPanel.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                _loadingAmount.fillAmount = progress;
                yield return null;
            }

            _loadingPanel.SetActive(false);            
        }
    }
}