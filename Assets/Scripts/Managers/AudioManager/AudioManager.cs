using System;
using Managers;
using UnityEngine;
using UnityEngine.Audio;

// From Brackeys. Reasoning: I like having a single audio manager as a Singleton to control all my audio. 
// The code isn't anything complicated; it just makes things easier to control for me.
namespace Managers
{
	public class AudioManager : MonoBehaviour
	{
		public static AudioManager instance;
		public AudioMixerGroup mixerGroup;
		public Sound[] sounds;

		void Awake ()
		{
			if (instance != null)
			{
				Destroy (gameObject);
			}
			else
			{
				instance = this;
				DontDestroyOnLoad (gameObject);
			}

			foreach (Sound s in sounds)
			{
				s._source = gameObject.AddComponent<AudioSource> ();
				s._source.clip = s._clip;
				s._source.loop = s._loop;

				s._source.outputAudioMixerGroup = mixerGroup;
			}
		}

		public void StopSound (string sound)
		{
			Sound s = Array.Find (sounds, item => item._name == sound);
			if (s == null)
			{
				Debug.LogWarning ("Sound: " + name + " not found!");
				return;
			}
			s._source.Stop ();
			s._source.volume = 0.0f;
		}

		public void Play (string sound)
		{
			Sound s = Array.Find (sounds, item => item._name == sound);
			if (s == null)
			{
				Debug.LogWarning ("Sound: " + name + " not found!");
				return;
			}

			s._source.volume = s._volume * (1f + UnityEngine.Random.Range (-s._volumeVariance / 2f, s._volumeVariance / 2f));
			s._source.pitch = s._pitch * (1f + UnityEngine.Random.Range (-s._pitchVariance / 2f, s._pitchVariance / 2f));

			s._source.Play ();
		}

	}
}