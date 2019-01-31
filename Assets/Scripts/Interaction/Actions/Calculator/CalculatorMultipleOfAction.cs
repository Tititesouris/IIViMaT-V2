using System;
using Interaction.Actors;
using UnityEngine;

namespace Interaction.Actions.Calculator
{
    public class CalculatorMultipleOfAction : CalculatorAction
    {
        [Tooltip("The value the calculator must be a multiple of to trigger the action.")]
        public float value = 2f;

        public bool Trigger(Actor actor, float calculatorValue)
        {
            if (!isActiveAndEnabled)
                return false;

            if (Math.Abs(calculatorValue % value) < float.Epsilon)
            {
                foreach (var reaction in GetSpecifiedReactions()) reaction.Trigger(actor, null);
                return true;
            }

            return false;
        }
    }
}