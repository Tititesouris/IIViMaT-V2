using UnityEngine;

namespace Interaction.Actions
{
    public class GazeAction : Action
    {

        // TODO Option to Allow/Prevent ray to go through objects between actor and target
        
        public bool Trigger(Actor actor, RaycastHit hit)
        {
            foreach (var reaction in Reactions)
            {
                reaction.ReactToAction();
            }

            return true;
        }
    }
}