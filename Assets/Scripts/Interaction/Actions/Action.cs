using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interaction.Reactions;
using UnityEngine;

namespace Interaction.Actions
{
    public abstract class Action : MonoBehaviour
    {
        // TODO: Give option to only trigger reactions with an AND or a XOR of actions. Or maybe a metaReaction AND and XOR?
        
        public bool specifyReactions;

        public List<string> reactionNames = new List<string>();

        protected List<Reaction> Reactions = new List<Reaction>();

        protected void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
            gameObject.tag = "Interactable";
        }

        protected void Start()
        {
            UpdateReactions();
        }
        
        private void UpdateReactions()
        {
            var reactions = GetComponents<Reaction>();

            if (specifyReactions)
            {
                foreach (var reaction in reactions)
                    if (reactionNames.Contains(reaction.reactionName, StringComparer.OrdinalIgnoreCase))
                        Reactions.Add(reaction);
            }
            else
                Reactions = new List<Reaction>(reactions);
        }

        public void StopTrigger()
        {
            foreach (var reaction in Reactions) reaction.StopTrigger();
        }
    }
}