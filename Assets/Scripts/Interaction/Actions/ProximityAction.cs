using Interaction.Actors;
using UnityEngine;

namespace Interaction.Actions
{
    public class ProximityAction : Action
    {

        [Tooltip("The reactions will be triggered if the actor is within this distance.")]
        public float triggerDistance = 1f;


        public bool Trigger(Actor actor)
        {
            if (!isActiveAndEnabled)
                return false;
            
            if ((actor.transform.position - transform.position).magnitude <= triggerDistance)
            {
                foreach (var reaction in Reactions) reaction.Trigger(actor, null);

                return true;
            }

            return false;
        }
    }
}