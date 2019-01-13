using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Video
{
	public class StopVideoReaction : VideoReaction
	{
		[Tooltip("If enabled, the video will stop after finishing the current loop.")]
		public bool stopAfterLoop;

		protected override bool React(Actor actor, RaycastHit? hit)
		{
			if (stopAfterLoop)
				VideoPlayer.isLooping = false;
			else
				VideoPlayer.Stop();
			return true;
		}
	}
}