using Interaction.Actors;
using UnityEngine;

namespace Interaction.Actions
{
    public class ProximityAction : Action
    {
        [Tooltip(
            "If enabled, the reactions will only be triggered if the actor is within a distance of [Trigger Distance].")]
        public bool triggerOnlyInRange;

        [Tooltip("The reactions will be triggered if the actor is within this distance.")]
        public float triggerDistance = 1f;


        public bool Trigger(Actor actor)
        {
            if (!triggerOnlyInRange || (actor.transform.position - transform.position).magnitude < triggerDistance)
            {
                foreach (var reaction in Reactions) reaction.ReactToAction(actor, null);

                return true;
            }

            return false;
        }
    }
}