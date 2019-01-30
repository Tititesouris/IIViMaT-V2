using Interaction.Actors;
using UnityEngine;

namespace Interaction.Actions.Meta
{
    public class PropagatedAction : Action
    {
        [Tooltip(
            "If enabled, the reactions will only be triggered if the propagator is within a distance of [Trigger Distance].")]
        public bool triggerOnlyInRange;
        
        [Tooltip("The reactions will be triggered if the propagator is within this distance.")]
        public float triggerDistance = 1f;

        public bool Trigger(Actor actor, RaycastHit? hit, GameObject propagator)
        {
            if (!isActiveAndEnabled)
                return false;
            
            if (!triggerOnlyInRange || (propagator.transform.position - transform.position).magnitude < triggerDistance)
            {
                foreach (var reaction in GetSpecifiedReactions()) reaction.Trigger(actor, hit);

                return true;
            }

            return false;
        }
    }
}