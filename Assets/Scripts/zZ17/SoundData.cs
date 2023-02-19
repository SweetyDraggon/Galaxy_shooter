using System;
using UnityEngine;

namespace zZ17
{
	[Serializable]
	public class SoundData
	{
		[SerializeField]
		public string name;

		[SerializeField]
		public AudioClip clip;

		public SoundData(string name, AudioClip audio)
		{
			this.name = name;
			this.clip = audio;
		}
	}
}
