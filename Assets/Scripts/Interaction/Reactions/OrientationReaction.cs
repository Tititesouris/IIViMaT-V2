using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions
{
    public class OrientationReaction : Reaction
    {
        [Tooltip("The orientation the object will have after reacting.")]
        public Vector3 newOrientation;

        [Tooltip(
            "If enabled, the new orientation will be relative to the current orientation. If disabled, the new orientation will be relative to the world orientation.")]
        public bool relativeNewOrientation;

        [Tooltip(
            "If enabled, the new orientation will be relative to the orientation of the actor that triggered the action.")]
        public bool relativeToActor;

        [Tooltip(
            "If enabled, the new orientation will be relative to the orientation of the active camera.")]
        public bool relativeToCamera;

        [Tooltip(
            "If enabled, the new orientation will be a random orientation centered around [New Orientation] within a range of [Random Range].")]
        public bool randomNewOrientation;

        [Tooltip("The new orientation will be randomized within this range.")]
        public Vector3 randomRange = Vector3.one;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            var orientation = transform.rotation;

            if (relativeToActor || relativeToCamera)
            {
                var reference = relativeToActor ? actor.transform : Camera.current.transform;
                orientation = reference.rotation * Quaternion.Euler(newOrientation);
            }
            else if (relativeNewOrientation)
                orientation *= Quaternion.Euler(newOrientation);
            else
                orientation = Quaternion.Euler(newOrientation);

            if (randomNewOrientation)
                orientation *= Quaternion.Euler(new Vector3(
                    Random.Range(-randomRange.x, randomRange.x),
                    Random.Range(-randomRange.y, randomRange.y),
                    Random.Range(-randomRange.z, randomRange.z)
                ));
            transform.rotation = orientation;
            return true;
        }
    }
}