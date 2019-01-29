using System.Collections.Generic;
using Interaction.Actions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

namespace Interaction.Actors
{
    public class VideoActor : Actor
    {
        [Tooltip("The time in seconds between triggers of VideoTime actions.")]
        public float timeStep = 0.1f;

        private VideoPlayer _videoPlayer;

        private double _nextTime;

        private int _nbLooped;

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
                _nbLooped++;
                EndVideoTriggers();
            };
        }

        protected override List<Action> Act()
        {
            _triggeredActions = new List<Action>();

            if (_playing != _videoPlayer.isPlaying)
            {
                PlayPauseVideoTriggers(_videoPlayer.isPlaying);
            }

            _playing = _videoPlayer.isPlaying;
            if (_playing && _nextTime <= _videoPlayer.time)
            {
                VideoTimeTriggers();
                _nextTime = timeStep * (int) (1 + _videoPlayer.time / timeStep);
            }

            return _triggeredActions;
        }

        private bool PlayPauseVideoTriggers(bool play)
        {
            var triggered = false;
            var actions = GetComponents<Action>();
            foreach (var action in actions)
            {
                var playAction = action as PlayVideoAction;
                var pauseAction = action as PauseVideoAction;
                if (play && playAction != null)
                {
                    if (!_triggeredActions.Contains(action))
                        _triggeredActions.Add(action);
                    playAction.Trigger(this);
                    triggered = true;
                }
                else if (!play && pauseAction != null)
                {
                    if (!_triggeredActions.Contains(action))
                        _triggeredActions.Add(action);
                    pauseAction.Trigger(this);
                    triggered = true;
                }
            }

            return triggered;
        }

        private bool EndVideoTriggers()
        {
            var triggered = false;
            var actions = GetComponents<Action>();
            foreach (var action in actions)
            {
                var endVideoAction = action as EndVideoAction;
                if (endVideoAction != null)
                {
                    if (!_triggeredActions.Contains(action))
                        _triggeredActions.Add(action);
                    endVideoAction.Trigger(this);
                    triggered = true;
                }
            }

            return triggered;
        }

        private bool VideoTimeTriggers()
        {
            var triggered = false;
            var actions = GetComponents<Action>();
            foreach (var action in actions)
            {
                var videoTimeAction = action as VideoTimeAction;
                if (videoTimeAction != null)
                {
                    if (!_triggeredActions.Contains(action))
                        _triggeredActions.Add(action);
                    videoTimeAction.Trigger(this, _nbLooped, _videoPlayer.time);
                    triggered = true;
                }
            }

            return triggered;
        }
    }
}