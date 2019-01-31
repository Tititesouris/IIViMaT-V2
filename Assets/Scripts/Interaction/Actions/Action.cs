using System.Collections.Generic;
using System.Linq;
using Interaction.Reactions;
using UnityEngine;

namespace Interaction.Actions
{
    public abstract class Action : MonoBehaviour
    {
        private static int _nextActionId = 1;

        public string actionName;

        public bool triggerOtherObject;

        public GameObject objectToTrigger;

        [Tooltip("If enabled, the action will also trigger reactions on objects of this group.")]
        public bool groupTrigger;

        public bool specifyReactions;

        [SerializeField] private List<Reaction> reactions = new List<Reaction>();

        public Action()
        {
            // TODO Separate Models and Monobehaviors, because using a constructor here is really weird.
            actionName = GetType().Name + " " + _nextActionId++;
        }

        protected void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
            gameObject.tag = "Interactable";
        }

        public List<Reaction> GetTargetedReactions()
        {
            var targetedReactions = new List<Reaction>();
            if (!triggerOtherObject)
            {
                targetedReactions = new List<Reaction>(
                    groupTrigger
                        ? GetComponentsInChildren<Reaction>()
                        : GetComponents<Reaction>()
                );
            }
            else if (objectToTrigger != null)
            {
                targetedReactions = new List<Reaction>(
                    groupTrigger
                        ? objectToTrigger.GetComponentsInChildren<Reaction>()
                        : objectToTrigger.GetComponents<Reaction>()
                );
            }

            targetedReactions.RemoveAll(reaction => reaction == null);
            return targetedReactions;
        }

        public List<Reaction> GetSpecifiedReactions()
        {
            if (!specifyReactions)
                reactions = GetTargetedReactions();
            return reactions;
        }

        public void SetSpecifiedReaction(List<Reaction> reactions)
        {
            this.reactions = reactions;
        }

        public void StopTrigger()
        {
            foreach (var reaction in GetSpecifiedReactions()) reaction.StopTrigger();
        }
    }
}