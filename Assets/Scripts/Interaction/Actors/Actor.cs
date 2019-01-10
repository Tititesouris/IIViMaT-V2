using System.Collections.Generic;
using Interaction.Actions;
using UnityEngine;

namespace Interaction.Actors
{
    public abstract class Actor : MonoBehaviour
    {
        private List<Action> _lastTriggeredActions;

        protected abstract List<Action> Act();

        private void Start()
        {
            _lastTriggeredActions = new List<Action>();
        }

        private void Update()
        {
            var triggeredActions = Act();
            foreach (var action in _lastTriggeredActions)
            {
                if (!triggeredActions.Contains(action))
                    action.StopTrigger();
            }
            _lastTriggeredActions = triggeredActions;
        }
    }
}