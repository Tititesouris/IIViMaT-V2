using System.Collections.Generic;
using Interaction.Reactions;
using UnityEngine;

namespace Interaction.Actions
{
    public abstract class Action : MonoBehaviour
    {
        // TODO: Give option to only trigger reactions with an AND or a XOR of actions. Or maybe a metaReaction AND and XOR?

        [Tooltip("If enabled, the action will also trigger reactions on objects of this group.")]
        public bool groupTrigger;

        public bool specifyReactions;

        public List<Reaction> reactions;

        protected void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
            gameObject.tag = "Interactable";
        }

        private void Update()
        {
            if (!specifyReactions)
            {
                reactions = new List<Reaction>(groupTrigger ? GetComponentsInChildren<Reaction>() : GetComponents<Reaction>());
            }
        }

        public void StopTrigger()
        {
            foreach (var reaction in reactions) reaction.StopTrigger();
        }
    }
}