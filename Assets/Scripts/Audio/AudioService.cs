using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Audio
{
	public class AudioService
	{
		private readonly AudioSource _source;
		private readonly Dictionary<string, AudioClip> _clipsByKeys;

		public AudioService(AudioSource source, AudioClipsRepository clipsRepository)
		{
			_source = source;
			_clipsByKeys = clipsRepository.Clips.ToDictionary(clip => clip.name, clip => clip);
		}

		public void Play(string clipName)
		{
			if (!_clipsByKeys.TryGetValue(clipName, out var clip))
				return;

			_source.PlayOneShot(clip);
		}
	}
}
