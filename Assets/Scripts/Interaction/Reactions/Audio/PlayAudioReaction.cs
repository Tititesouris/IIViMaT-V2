using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Audio
{
    public class PlayAudioReaction : AudioReaction
    {
        [Tooltip("If enabled, the audio will loop.")]
        public bool loop;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            AudioPlayer.loop = loop;
            AudioPlayer.Play();
            return true;
        }
    }
}