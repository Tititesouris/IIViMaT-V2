using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Video
{
	public class PlayVideoReaction : VideoReaction
	{
		protected override bool React(Actor actor, RaycastHit? hit)
		{
			VideoPlayer.Play();
			return true;
		}
	}
}