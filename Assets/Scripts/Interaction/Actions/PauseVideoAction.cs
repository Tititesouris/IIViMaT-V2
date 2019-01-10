using System;
using Interaction.Actors;

namespace Interaction.Actions
{
    public class PauseVideoAction : Action
    {
        public bool Trigger(Actor actor)
        {
            foreach (var reaction in Reactions) reaction.Trigger(actor, null);

            return true;
        }
    }
}