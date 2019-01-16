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

        private bool _triggeredStart;

        private double _nextTime;

        private int _nbLooped;

        public bool Trigger(Actor actor, int nbLooped, double videoTime)
        {
            if (!isActiveAndEnabled)
                return false;

            if (nbLooped > _nbLooped)
            {
                _nbLooped++;
                _triggeredStart = false;
            }
            if (videoTime >= startTime)
            {
                if (_triggeredStart)
                {
                    if (repeat && videoTime >= _nextTime)
                    {
                        _nextTime += interval;
                        return TriggerTime(actor);
                    }
                }
                else
                {
                    _triggeredStart = true;
                    _nextTime = startTime + interval;
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