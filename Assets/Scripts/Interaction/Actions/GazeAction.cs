using Interaction.Actors;
using UnityEngine;

namespace Interaction.Actions
{
    public class GazeAction : Action
    {
        public bool Trigger(Actor actor, RaycastHit hit)
        {
            foreach (var reaction in Reactions) reaction.Trigger(actor, hit);

            return true;
        }
    }
}