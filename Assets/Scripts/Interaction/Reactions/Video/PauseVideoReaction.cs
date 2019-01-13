using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Video
{
    public class PauseVideoReaction : VideoReaction {

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            VideoPlayer.Pause();
            return true;
        }
    }
}
