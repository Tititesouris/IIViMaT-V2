using System.Collections.Generic;
using Interaction.Actions;
using Interaction.Actions.Calculator;
using Interaction.Actions.Clock;
using UnityEditor;
using UnityEngine;

namespace Interaction.Actors
{
    public class CalculatorActor : Actor
    {
        private List<Action> _triggeredActions;

        private Calculator _calculator;

        private float _lastValue;

        protected new void Awake()
        {
            base.Awake();
            _calculator = GetComponent<Calculator>();
            if (_calculator == null)
            {
                EditorUtility.DisplayDialog("Error", "Calculator Actor is missing Calculator:\n" +
                                                     "Make sure that a Calculator is present on the same object as CalculatorActor.",
                    "Ok");
                EditorApplication.isPlaying = false;
            }
            else
            {
                _lastValue = _calculator.value;
            }
        }

        protected override List<Action> Act()
        {
            _triggeredActions = new List<Action>();
            if (!Equals(_calculator.value, _lastValue))
            {
                CalculatorTriggers();
                _lastValue = _calculator.value;
            }

            return _triggeredActions;
        }

        private bool CalculatorTriggers()
        {
            var actions = GetComponents<Action>();
            foreach (var action in actions)
            {
                var equalsToAction = action as CalculatorEqualsToAction;
                var multipleOfAction = action as CalculatorMultipleOfAction;
                if (equalsToAction != null)
                {
                    if (!_triggeredActions.Contains(action))
                        _triggeredActions.Add(action);

                    equalsToAction.Trigger(this, _calculator.value);
                }
                else if (multipleOfAction != null)
                {
                    if (!_triggeredActions.Contains(action))
                        _triggeredActions.Add(action);

                    multipleOfAction.Trigger(this, _calculator.value);
                }
            }

            return actions.Length > 0;
        }
    }
}