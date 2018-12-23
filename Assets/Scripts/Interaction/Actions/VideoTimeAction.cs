using Interaction.Actors;
using UnityEngine;

namespace Interaction.Actions
{
    public class VideoTimeAction : Action
    {
        private double _lastTime;

        [Tooltip("If enabled, react every [Time] seconds of the video.")]
        public bool repeat;

        [Tooltip("The time in seconds after the start of the video to wait before reacting.")]
        public float time = 10f;

        public bool Trigger(Actor actor, double videoTime)
        {
            if ((repeat || _lastTime < time) && _lastTime + time <= videoTime)
            {
                foreach (var reaction in Reactions) reaction.ReactToAction(actor, null);
                _lastTime = time * (int) (videoTime / time);

                return true;
            }

            return false;
        }
    }
}