using System.Collections.Generic;
using Interaction.Actions;
using UnityEngine;
using UnityEngine.Video;

namespace Interaction.Actors
{
    public class VideoActor : Actor
    {

        // TODO: Option Trigger every x seconds

        // TODO: Trigger on play/pause/stop

        [Tooltip("If enabled the actor will trigger PlayVideo action.")]
        public bool triggerPlayVideoActions = true;

        [Tooltip("If enabled the actor will trigger PauseVideo action.")]
        public bool triggerPauseVideoActions = true;

        [Tooltip("If enabled the actor will trigger VideoTime action.")]
        public bool triggerVideoTimeActions = true;

        [Tooltip("The time in seconds between every VideoTime action.")]
        public float timeStep = 1f;
        
        private VideoPlayer _videoPlayer;
        
        private AudioSource _audioPlayer;

        private double _lastTime;

        private bool _playing;
        
        private List<Action> _triggeredActions;

        protected override List<Action> Act()
        {
            _triggeredActions = new List<Action>();
            var interactables = GameObject.FindGameObjectsWithTag("Interactable");
            if (_playing != _videoPlayer.isPlaying)
            {
                if (triggerPlayVideoActions && !_playing)
                    PlayVideoTriggers(interactables);
                if (triggerPauseVideoActions && _playing)
                    PauseVideoTriggers(interactables);
            }

            _playing = _videoPlayer.isPlaying;
            if (triggerVideoTimeActions && _lastTime + timeStep <= _videoPlayer.time) // TODO: Reset lastTime to 0 on video loop
            {
                VideoTimeTriggers(interactables);
                _lastTime = timeStep * (int)(_videoPlayer.time / timeStep);
            }

            return _triggeredActions;
        }

        private void Start()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
            _audioPlayer = GetComponent<AudioSource>();
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
                        if (!_triggeredActions.Contains(action))
                            _triggeredActions.Add(action);
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
                        if (!_triggeredActions.Contains(action))
                            _triggeredActions.Add(action);
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
                        if (!_triggeredActions.Contains(action))
                            _triggeredActions.Add(action);
                        videoTimeAction.Trigger(this, _videoPlayer.time);
                        triggered = true;
                    }
                }
            }

            return triggered;
        }
    }
}