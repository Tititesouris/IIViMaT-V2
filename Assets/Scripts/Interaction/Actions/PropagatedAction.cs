using UnityEngine;

namespace Interaction.Actions
{
    public class PropagatedAction : Action
    {
        [Tooltip("The reactions will be triggered if the propagator is within this distance.")]
        public float TriggerDistance = 1f;

        [Tooltip(
            "If enabled, the reactions will only be triggered if the propagator is within a distance of [TriggerDistance].")]
        public bool TriggerOnlyInRange;

        public bool Trigger(Actor actor, RaycastHit? hit, GameObject propagator)
        {
            if (!TriggerOnlyInRange || (propagator.transform.position - transform.position).magnitude < TriggerDistance)
            {
                foreach (var reaction in Reactions)
                {
                    reaction.ReactToAction(actor, hit);
                }

                return true;
            }

            return false;
        }
    }
}