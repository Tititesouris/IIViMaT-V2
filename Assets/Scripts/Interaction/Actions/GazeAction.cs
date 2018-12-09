using UnityEngine;

namespace Interaction.Actions
{
    public class GazeAction : Action
    {

        public bool Trigger(Vector3 contactPoint)
        {
            foreach (var reaction in Reactions)
            {
                reaction.React();
            }

            return true;
        }
    }
}