using System.Collections.Generic;
using Interaction.Actions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

namespace Interaction.Actors
{
    public class VideoActor : Actor
    {
        // TODO: Trigger on start/stop/loop

        [Tooltip("If enabled the actor will trigger PlayVideo actions when the video is played.")]
        public bool triggerPlayVideoActions = true;

        [Tooltip("If enabled the actor will trigger PauseVideo actions when the video is paused.")]
        public bool triggerPauseVideoActions = true;

        [Tooltip("If enabled the actor will trigger EndVideo actions when the video ends.")]
        public bool triggerEndVideoActions = true;

        [Tooltip("If enabled the actor will trigger VideoTime actions every interval of time of the video.")]
        public bool triggerVideoTimeActions = true;

        [Tooltip("The time in seconds between every VideoTime action.")]
        public float timeStep = 1f;

        private VideoPlayer _videoPlayer;

        private double _nextTime;

        private bool _playing;

        private List<Action> _triggeredActions;

        protected new void Awake()
        {
            base.Awake();
            _videoPlayer = GetComponent<VideoPlayer>();
            if (_videoPlayer == null)
            {
                EditorUtility.DisplayDialog("Error", "Video Actor can only be placed on a 360 Sphere:\n" +
                                                     GetType().Name + " placed on " + name, "Ok");
                EditorApplication.isPlaying = false;
            }

            if (_videoPlayer.clip == null)
            {
                EditorUtility.DisplayDialog("Error", "360 Sphere does not have any video to play:\n" +
                                                     name, "Ok");
                EditorApplication.isPlaying = false;
            }
        }

        protected new void Start()
        {
            base.Start();

            _videoPlayer.loopPointReached += source =>
            {
                _nextTime = 0;
                if (triggerEndVideoActions)
                    EndVideoTriggers();
            };
        }

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
            if (_playing && triggerVideoTimeActions && _nextTime <= _videoPlayer.time)
            {
                VideoTimeTriggers(interactables);
                _nextTime = timeStep * (int) (1 + _videoPlayer.time / timeStep);
            }

            return _triggeredActions;
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

        private bool StartVideoTriggers()
        {
// TODO Start triggers
            Debug.LogError("Not Implemented Yet: StartVideoTrigger");
            return false;
        }

        private bool EndVideoTriggers()
        {
// TODO End triggers
            Debug.LogError("Not Implemented Yet: EndVideoTrigger");
            return false;
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