using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Audio
{
	public class PauseAudioReaction : AudioReaction {

		protected override bool React(Actor actor, RaycastHit? hit)
		{
			AudioPlayer.Pause();
			return true;
		}
	}
}
