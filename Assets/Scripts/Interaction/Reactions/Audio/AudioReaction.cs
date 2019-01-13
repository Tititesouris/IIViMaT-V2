using UnityEngine;

namespace Interaction.Reactions.Audio
{
	public abstract class AudioReaction : Reaction {
		
		protected AudioSource AudioPlayer;
        
		protected new void Awake()
		{
			base.Awake();
			AudioPlayer = GetComponent<AudioSource>();
		}

		protected new void Start()
		{
			base.Start();
		}
	}
}
