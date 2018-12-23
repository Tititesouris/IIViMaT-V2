using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions
{
	public class PlayAudioReaction : Reaction {

		private AudioSource _audioPlayer;
        
		private void Start()
		{
			_audioPlayer = GetComponent<AudioSource>();
		}
		
		protected override bool React(Actor actor, RaycastHit? hit)
		{
			_audioPlayer.Play();
			return true;
		}
	}
}
