using System;
using System.Collections.Generic;
using System.Linq;
using Interaction.Reactions;
using UnityEngine;

namespace Interaction.Actions
{
    public class Action : MonoBehaviour
    {
        [Header("Reactions")] [Tooltip("If enabled, allows you to specify which reactions to trigger.")]
        public bool SpecifyReactions;

        [Tooltip("List all the reactions that will be triggered.")]
        public List<string> ReactionNames = new List<string>();


        protected List<Reaction> Reactions = new List<Reaction>();

        void Start()
        {
            Reaction[] reactions = GetComponents<Reaction>();
            if (!SpecifyReactions || ReactionNames.Count == 0)
            {
                Reactions = new List<Reaction>(reactions);
            }
            else
            {
                foreach (var reaction in reactions)
                {
                    if (ReactionNames.Contains(reaction.ReactionName, StringComparer.OrdinalIgnoreCase))
                    {
                        Reactions.Add(reaction);
                    }
                }
            }
        }
    }
}