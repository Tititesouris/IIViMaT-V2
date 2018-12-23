using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions
{
    public class OrientationReaction : Reaction
    {
        [Tooltip("The orientation the object will have after reacting.")]
        public Vector3 newOrientation;

        [Tooltip(
            "If enabled, the new orientation will be a random orientation centered around [New Orientation] within a range of [Random Range].")]
        public bool randomNewOrientation;

        [Tooltip("The new orientation will be randomized within this range.")]
        public Vector3 randomRange = Vector3.one;

        [Tooltip(
            "If enabled, the new orientation will be relative to the current orientation. If disabled, the new orientation will be relative to the world orientation.")]
        public bool relativeNewOrientation;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            var orientation = transform.localRotation.eulerAngles;
            if (relativeNewOrientation)
                orientation += newOrientation; // TODO Fix bug always positive angle
            else
                orientation = newOrientation;

            if (randomNewOrientation)
                orientation += new Vector3(
                    Random.Range(-randomRange.x, randomRange.x),
                    Random.Range(-randomRange.y, randomRange.y),
                    Random.Range(-randomRange.z, randomRange.z)
                );

            transform.localRotation = Quaternion.Euler(orientation);
            return true;
        }
    }
}