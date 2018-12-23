using System.Collections.Generic;
using Interaction.Actions;
using UnityEngine;
using UnityEngine.Video;

namespace Interaction.Actors
{
    public class VideoActor : Actor
    {
        private AudioSource _audioPlayer;

        private double _lastTime;

        private bool _playing;

        private VideoPlayer _videoPlayer;

        [Tooltip("The time in seconds between every VideoTime action.")]
        public float timeStep = 1f;

        [Tooltip("If enabled the actor will trigger PauseVideo action.")]
        public bool triggerPauseVideoActions = true;

        // TODO: Option Trigger every x seconds

        // TODO: Trigger on play/pause/stop

        [Tooltip("If enabled the actor will trigger PlayVideo action.")]
        public bool triggerPlayVideoActions = true;

        [Tooltip("If enabled the actor will trigger VideoTime action.")]
        public bool triggerVideoTimeActions = true;

        private void Start()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
            _audioPlayer = GetComponent<AudioSource>();
        }

        private void Update()
        {
            var interactables = GameObject.FindGameObjectsWithTag("Interactable");
            if (_playing != _videoPlayer.isPlaying)
            {
                if (triggerPlayVideoActions && !_playing)
                    PlayVideoTriggers(interactables);
                if (triggerPauseVideoActions && _playing)
                    PauseVideoTriggers(interactables);
            }

            _playing = _videoPlayer.isPlaying;
            if (triggerVideoTimeActions && _lastTime + timeStep <= _videoPlayer.time
            ) // TODO: Reset lastTime to 0 on video loop
            {
                Debug.Log(_videoPlayer.time);
                VideoTimeTriggers(interactables);
                _lastTime = timeStep * (int) (_videoPlayer.time / timeStep);
            }
        }

        private bool PlayVideoTriggers(IEnumerable<GameObject> interactables)
        {
            var triggered = false;
            foreach (var interactable in interactables)
            {
                var actions = interactable.GetComponents<Action>();
                foreach (var action in actions)
                {
                    var playAction = action as PlayVideoAction;
                    if (playAction != null)
                    {
                        playAction.Trigger(this);
                        triggered = true;
                    }
                }
            }

            return triggered;
        }

        private bool PauseVideoTriggers(IEnumerable<GameObject> interactables)
        {
            var triggered = false;
            foreach (var interactable in interactables)
            {
                var actions = interactable.GetComponents<Action>();
                foreach (var action in actions)
                {
                    var pauseAction = action as PauseVideoAction;
                    if (pauseAction != null)
                    {
                        pauseAction.Trigger(this);
                        triggered = true;
                    }
                }
            }

            return triggered;
        }

        private bool VideoTimeTriggers(IEnumerable<GameObject> interactables)
        {
            var triggered = false;
            foreach (var interactable in interactables)
            {
                var actions = interactable.GetComponents<Action>();
                foreach (var action in actions)
                {
                    var videoTimeAction = action as VideoTimeAction;
                    if (videoTimeAction != null)
                    {
                        videoTimeAction.Trigger(this, _videoPlayer.time);
                        triggered = true;
                    }
                }
            }

            return triggered;
        }
    }
}