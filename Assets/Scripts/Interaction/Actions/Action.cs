using System;
using System.Collections.Generic;
using System.Linq;
using Interaction.Reactions;
using UnityEngine;

namespace Interaction.Actions
{
    public abstract class Action : MonoBehaviour
    {
        [Tooltip("List all the reactions that will be triggered.")]
        public List<string> reactionNames = new List<string>();

        // TODO: Give option to only trigger reactions with an AND or a XOR of actions

        protected List<Reaction> Reactions = new List<Reaction>();

        [Header("Reactions")] [Tooltip("If enabled, allows you to specify which reactions to trigger.")]
        public bool specifyReactions;

        private void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
            gameObject.tag = "Interactable"; // TODO: If video360 is interactable, it loses its 360Video tag
            UpdateReactions();
        }

        private void UpdateReactions()
        {
            var reactions = GetComponents<Reaction>();

            if (specifyReactions)
                foreach (var reaction in reactions)
                    if (reactionNames.Contains(reaction.reactionName, StringComparer.OrdinalIgnoreCase))
                        Reactions.Add(reaction);
                    else
                        Reactions = new List<Reaction>(reactions);
        }
    }
}