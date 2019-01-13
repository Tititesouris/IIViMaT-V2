﻿using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Interaction.Reactions.Camera
{
    public abstract class CameraReaction : Reaction
    {
        protected static PostProcessEffectSettings[] Effects;

        protected IEnumerator EffectCoroutine;

        private static PostProcessVolume _volume;

        protected new void Awake()
        {
            base.Awake();
            if (GetComponent<UnityEngine.Camera>() == null)
            {
                EditorUtility.DisplayDialog("Error", "Camera Reactions can only be placed on cameras:\n" +
                                                     GetType().Name + " placed on " + name, "Ok");
                EditorApplication.isPlaying = false;
            }
        }

        protected new void Start()
        {
            base.Start();
            if (_volume == null)
            {
                Effects = new PostProcessEffectSettings[]
                {
                    ScriptableObject.CreateInstance<AmbientOcclusion>(),
                    ScriptableObject.CreateInstance<AutoExposure>(),
                    ScriptableObject.CreateInstance<Bloom>(),
                    ScriptableObject.CreateInstance<ChromaticAberration>(),
                    ScriptableObject.CreateInstance<ColorGrading>(),
                    ScriptableObject.CreateInstance<DepthOfField>(),
                    ScriptableObject.CreateInstance<Grain>(),
                    ScriptableObject.CreateInstance<LensDistortion>(),
                    ScriptableObject.CreateInstance<MotionBlur>(),
                    ScriptableObject.CreateInstance<ScreenSpaceReflections>(),
                    ScriptableObject.CreateInstance<Vignette>()
                };
                _volume = PostProcessManager.instance.QuickVolume(LayerMask.NameToLayer("Post Processing"), 100f,
                    Effects);
            }
        }

        private void OnDestroy()
        {
            if (_volume != null)
                RuntimeUtilities.DestroyVolume(_volume, true, true);
        }

        protected virtual void StartEffect(){}

        protected virtual void ApplyEffect(float value){}

        protected virtual void ResetEffect(){}

        protected IEnumerator Effect(float startTime, float duration, bool clearAfter)
        {
            while (Time.time < startTime + duration)
            {
                ApplyEffect((Time.time - startTime) / duration);
                yield return null;
            }

            ApplyEffect(1);
            if (clearAfter) ResetEffect();
        }
    }
}