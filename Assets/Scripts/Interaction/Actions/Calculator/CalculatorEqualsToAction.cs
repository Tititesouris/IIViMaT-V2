using Interaction.Actors;
using UnityEngine;

namespace Interaction.Actions.Calculator
{
    public class CalculatorEqualsToAction : CalculatorAction
    {
        [Tooltip("The value the calculator must be equal to for the action to be triggered.")]
        public float value;

        public bool Trigger(Actor actor, float calculatorValue)
        {
            if (!isActiveAndEnabled)
                return false;

            if (Equals(calculatorValue, value))
            {
                foreach (var reaction in GetSpecifiedReactions()) reaction.Trigger(actor, null);
                return true;
            }

            return false;
        }
    }
}