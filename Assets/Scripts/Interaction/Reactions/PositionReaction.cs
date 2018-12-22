using UnityEngine;

namespace Interaction.Reactions
{
    public class PositionReaction : Reaction
    {
        [Tooltip("The new position of the object after reacting.")]
        public Vector3 NewPosition;

        [Tooltip(
            "If enabled, the new position will be a random position centered around [New Position] within a range of [Random Range].")]
        public bool RandomNewPosition;

        [Tooltip("The new position will be randomized within this range.")]
        public Vector3 RandomRange = Vector3.one;

        [Tooltip(
            "If enabled, the new position will be relative to the current position. If disabled, the new position will be relative to world coordinates.")]
        public bool RelativeNewPosition;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            var newPosition = transform.position;
            if (RelativeNewPosition)
            {
                newPosition += NewPosition;
            }
            else
            {
                newPosition = NewPosition;
            }

            if (RandomNewPosition)
            {
                newPosition += new Vector3(
                    Random.Range(-RandomRange.x, RandomRange.x),
                    Random.Range(-RandomRange.y, RandomRange.y),
                    Random.Range(-RandomRange.z, RandomRange.z)
                );
            }

            transform.position = newPosition;
            return true;
        }
    }
}