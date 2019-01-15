using System;
using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Audio
{
    public class ChangeVolumeAudioReaction : AudioReaction
    {
        public enum VolumeChangeOptions
        {
            ChangeValue,
            IncreaseValue,
            DecreaseValue
        }

        public VolumeChangeOptions volumeChange = VolumeChangeOptions.ChangeValue;

        [Range(0, 1)] public float volume = 0.5f;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            switch (volumeChange)
            {
                case VolumeChangeOptions.ChangeValue:
                    AudioPlayer.volume = volume;
                    break;
                case VolumeChangeOptions.IncreaseValue:
                    AudioPlayer.volume += volume;
                    break;
                case VolumeChangeOptions.DecreaseValue:
                    AudioPlayer.volume -= volume;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }
    }
}