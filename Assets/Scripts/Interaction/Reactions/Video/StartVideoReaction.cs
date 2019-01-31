using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Video
{
	public class StartVideoReaction : VideoReaction
	{
		[Tooltip("The starting time in the video.")]
		public float startTime;
		
		[Tooltip("If enabled, the video will loop.")]
		public bool loop;

		protected override bool React(Actor actor, RaycastHit? hit)
		{
			VideoPlayer.isLooping = loop;
			VideoPlayer.Play();
			VideoPlayer.time = startTime;
			return true;
		}
	}
}