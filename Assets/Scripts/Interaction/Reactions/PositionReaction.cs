using UnityEngine;

namespace Interaction.Reactions
{
    public class PositionReaction : Reaction
    {
        [Tooltip("The position the object will have after reacting.")]
        public Vector3 NewPosition;

        public override bool React()
        {
            transform.position = NewPosition;
            return true;
        }
    }
}