using UnityEngine;
using UnityEngine.Video;

namespace Interaction.Reactions.Video
{
    public abstract class VideoReaction : Reaction {
		
        protected VideoPlayer VideoPlayer;
        
        protected AudioSource AudioPlayer;

        protected new void Awake()
        {
            base.Awake();
        }

        protected new void Start()
        {
            base.Start();
            VideoPlayer = GetComponent<VideoPlayer>();
            AudioPlayer = GetComponent<AudioSource>();
        }
    }
}
