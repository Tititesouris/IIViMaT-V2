using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions
{
    public class PositionReaction : Reaction
    {
        [Tooltip("The new position of the object after reacting.")]
        public Vector3 newPosition;

        [Tooltip(
            "If enabled, the new position will be relative to the current position. If disabled, the new position will be relative to world coordinates.")]
        public bool relativeNewPosition;

        [Tooltip(
            "If enabled, the new position will be relative to the position of the actor that triggered the action. If disabled, the new position will be relative to world coordinates.")]
        public bool relativeToActor;

        [Tooltip(
            "If enabled, the new position will be relative to the heading of the actor that triggered the action. If disabled, the new position will be relative to world orientation.")]
        public bool relativeToActorHeading;

        [Tooltip(
            "If enabled, the new position will be a random position centered around [New Position] within a range of [Random Range].")]
        public bool randomNewPosition;

        [Tooltip("The new position will be randomized within this range.")]
        public Vector3 randomRange = Vector3.one;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            var position = transform.position;
            if (relativeToActor)
            {
                position = actor.transform.position + newPosition;
                if (relativeToActorHeading)
                    position = Quaternion.Euler(actor.transform.eulerAngles) * (position - actor.transform.position) +
                               actor.transform.position;
            }
            else if (relativeNewPosition)
            {
                position += newPosition;
            }
            else
            {
                position = newPosition;
            }

            if (randomNewPosition)
                position += new Vector3(
                    Random.Range(-randomRange.x, randomRange.x),
                    Random.Range(-randomRange.y, randomRange.y),
                    Random.Range(-randomRange.z, randomRange.z)
                );

            transform.position = position;
            return true;
        }
    }
}