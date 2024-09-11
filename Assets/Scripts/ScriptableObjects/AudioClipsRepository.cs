using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
	[CreateAssetMenu(fileName = "AudioClipsRepository", menuName = "Game/Repositories/AudioClipsRepository")]
	public class AudioClipsRepository : ScriptableObject
	{
		public List<AudioClip> Clips => _clips;
		[SerializeField] private List<AudioClip> _clips;
	}
}
