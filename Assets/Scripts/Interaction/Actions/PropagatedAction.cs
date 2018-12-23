using Interaction.Actors;
using UnityEngine;

namespace Interaction.Actions
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
            if (!triggerOnlyInRange || (propagator.transform.position - transform.position).magnitude < triggerDistance)
            {
                foreach (var reaction in Reactions) reaction.ReactToAction(actor, hit);

                return true;
            }

            return false;
        }
    }
}