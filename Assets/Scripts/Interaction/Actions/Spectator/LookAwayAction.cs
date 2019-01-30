using Interaction.Actors;
using UnityEngine;

namespace Interaction.Actions.Spectator
{
    public class LookAwayAction : Action
    {
        public bool Trigger(Actor actor, RaycastHit hit)
        {
            if (!isActiveAndEnabled)
                return false;

            foreach (var reaction in GetSpecifiedReactions()) reaction.Trigger(actor, hit);

            return true;
        }
    }
}