using UnityEngine;

namespace Interaction.Actions
{
    public class GazeAction : Action
    {
        public bool Trigger(Actor actor, RaycastHit hit)
        {
            foreach (var reaction in Reactions)
            {
                reaction.ReactToAction(actor, hit);
            }

            return true;
        }
    }
}