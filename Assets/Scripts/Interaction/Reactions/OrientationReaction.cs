using UnityEngine;

namespace Interaction.Reactions
{
    public class OrientationReaction : Reaction
    {
        [Tooltip("The orientation the object will have after reacting.")]
        public Vector3 NewOrientation;

        [Tooltip(
            "If enabled, the new orientation will be relative to the current orientation. If disabled, the new orientation will be relative to the world orientation.")]
        public bool RelativeNewOrientation;

        [Tooltip(
            "If enabled, the new orientation will be a random orientation centered around [New Orientation] within a range of [Random Range].")]
        public bool RandomNewOrientation;

        [Tooltip("The new orientation will be randomized within this range.")]
        public Vector3 RandomRange = Vector3.one;

        protected override bool React()
        {
            var newOrientation = transform.localRotation.eulerAngles;
            if (RelativeNewOrientation)
            {
                newOrientation += NewOrientation; // TODO rounded to a halt
            }
            else
            {
                newOrientation = NewOrientation;
            }

            if (RandomNewOrientation)
            {
                newOrientation += new Vector3(
                    Random.Range(-RandomRange.x, RandomRange.x),
                    Random.Range(-RandomRange.y, RandomRange.y),
                    Random.Range(-RandomRange.z, RandomRange.z)
                );
            }

            transform.localRotation = Quaternion.Euler(newOrientation);
            return true;
        }
    }
}