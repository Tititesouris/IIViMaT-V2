using System;
using Interaction.Actors;
using UnityEngine;

namespace Interaction.Actions
{
    public class VideoTimeAction : Action
    {
        [Tooltip("The time in seconds after the start of the video to wait before reacting.")]
        public float startTime = 10f;

        [Tooltip("If enabled, react every [Interval] seconds of the video after [Time] seconds have passed.")]
        public bool repeat;
        
        [Tooltip("The time in seconds between each repeat.")]
        public float interval = 1f;

        private bool _triggered;
        
        private double _lastInterval;

        public bool Trigger(Actor actor, double videoTime)
        {
            if (!isActiveAndEnabled)
                return false;
            
            if (_triggered)
            {
                if (repeat && startTime + _lastInterval + interval <= videoTime)
                {
                    _lastInterval = interval * (int)((videoTime - startTime) / interval);
                    return TriggerTime(actor);
                }
            }
            else
            {
                if (videoTime >= startTime)
                {
                    _triggered = true;
                    return TriggerTime(actor);
                }
            }
            return false;
        }

        private bool TriggerTime(Actor actor)
        {
            foreach (var reaction in Reactions) reaction.Trigger(actor, null);
            return true;
        }
    }
}