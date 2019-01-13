using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Audio
{
    public class StopAudioReaction : AudioReaction
    {
        [Tooltip("If enabled, the audio will stop after finishing the current loop.")]
        public bool stopAfterLoop;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            if (stopAfterLoop)
                AudioPlayer.loop = false;
            else
                AudioPlayer.Stop();
            return true;
        }
    }
}