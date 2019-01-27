using System;
using Interaction.Actors;

namespace Interaction.Actions
{
    public class PauseVideoAction : Action
    {
        public bool Trigger(Actor actor)
        {
            if (!isActiveAndEnabled)
                return false;
            
            foreach (var reaction in reactions) reaction.Trigger(actor, null);

            return true;
        }
    }
}