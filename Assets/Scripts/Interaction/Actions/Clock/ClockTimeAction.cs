using Interaction.Actors;
using UnityEngine;

namespace Interaction.Actions.Clock
{
    public class ClockTimeAction : Action
    {
        [Tooltip("The time in seconds after the start of the clock to wait before reacting.")]
        public float startTime = 10f;

        [Tooltip("If enabled, react every [Interval] seconds of the video after [Time] seconds have passed.")]
        public bool repeat;

        [Tooltip("The time in seconds between each repeat.")]
        public float interval = 1f;

        private bool _triggeredStart;

        private double _nextTime;

        public bool Trigger(Actor actor, float clockTime)
        {
            if (!isActiveAndEnabled)
                return false;

            if (clockTime >= startTime)
            {
                if (!_triggeredStart || repeat && clockTime >= _nextTime)
                {
                    Debug.Log(clockTime);
                    _nextTime = startTime + interval * Mathf.Floor((clockTime - startTime) / interval + 1);
                    _triggeredStart = true;
                    foreach (var reaction in GetSpecifiedReactions()) reaction.Trigger(actor, null);
                    return true;
                }
            }

            return false;
        }
    }
}