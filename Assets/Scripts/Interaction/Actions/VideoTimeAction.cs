using System;
using Interaction.Actors;
using UnityEngine;

namespace Interaction.Actions
{
    public class VideoTimeAction : Action
    {
        [Tooltip("The time in seconds after the start of the video to wait before reacting.")]
        public float time = 10f;

        [Tooltip("If enabled, react every [Interval] seconds of the video after [Time] seconds have passed.")]
        public bool repeat;
        
        [Tooltip("The time in seconds between each repeat.")]
        public float interval = 1f;

        private bool _triggered;
        
        private double _lastInterval; // TODO: Bug, doesn't reset when video loops

        public bool Trigger(Actor actor, double videoTime)
        {
            if (_triggered)
            {
                if (repeat && time + _lastInterval + interval <= videoTime)
                {
                    _lastInterval = interval * (int)((videoTime - time) / interval);
                    return TriggerTime(actor);
                }
            }
            else
            {
                if (videoTime >= time)
                {
                    _triggered = true;
                    return TriggerTime(actor);
                }
            }
            return false;
        }

        private bool TriggerTime(Actor actor)
        {
            foreach (var reaction in Reactions) reaction.ReactToAction(actor, null);
            return true;
        }
    }
}