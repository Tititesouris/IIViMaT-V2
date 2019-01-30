using System;
using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Calculator
{
    public class CalculateReaction : Reaction
    {
        
        public enum OperationOptions
        {
            Add,
            Subtract,
            Multiply,
            Divide
        }

        public global::Calculator calculator;
        
        public OperationOptions operation = OperationOptions.Add;

        public float value;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            if (calculator == null)
                return false;
            switch (operation)
            {
                case OperationOptions.Add:
                    return calculator.Add(value);
                case OperationOptions.Subtract:
                    return calculator.Subtract(value);
                case OperationOptions.Multiply:
                    return calculator.Multiply(value);
                case OperationOptions.Divide:
                    return calculator.Divide(value);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}