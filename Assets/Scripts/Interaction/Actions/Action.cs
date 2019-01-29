using System.Collections.Generic;
using System.Linq;
using Interaction.Reactions;
using UnityEngine;

namespace Interaction.Actions
{
    public abstract class Action : MonoBehaviour
    {
        // TODO: Give option to only trigger reactions with an AND or a XOR of actions. Or maybe a metaReaction AND and XOR?
        private static int _nextActionId = 1;

        public string actionName;

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

        public List<Reaction> GetSpecifiedReactions()
        {
            if (!specifyReactions)
                reactions = new List<Reaction>(
                    groupTrigger ? GetComponentsInChildren<Reaction>() : GetComponents<Reaction>()
                );
            reactions.RemoveAll(reaction => reaction == null);
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