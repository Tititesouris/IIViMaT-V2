using System;
using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Appearance
{
    public class TransparencyReaction : Reaction
    {
        public enum TransparencyModeOptions
        {
            Real,
            Virtual
        }

        [Range(0, 1)] public float transparency = 0.5f;

        public TransparencyModeOptions transparencyMode = TransparencyModeOptions.Real;

        private Material _material;

        private static readonly int SrcBlend = Shader.PropertyToID("_SrcBlend");

        private static readonly int DstBlend = Shader.PropertyToID("_DstBlend");

        private static readonly int ZWrite = Shader.PropertyToID("_ZWrite");

        protected new void Awake()
        {
            base.Awake();
            _material = GetComponent<Renderer>().material;
        }

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            switch (transparencyMode)
            {
                case TransparencyModeOptions.Real:
                    _material.SetInt(SrcBlend, (int) UnityEngine.Rendering.BlendMode.One);
                    _material.SetInt(DstBlend, (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    _material.SetInt(ZWrite, 0);
                    _material.DisableKeyword("_ALPHATEST_ON");
                    _material.DisableKeyword("_ALPHABLEND_ON");
                    _material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    _material.renderQueue = 3000;
                    break;
                case TransparencyModeOptions.Virtual:
                    _material.SetInt(SrcBlend, (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
                    _material.SetInt(DstBlend, (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    _material.SetInt(ZWrite, 0);
                    _material.DisableKeyword("_ALPHATEST_ON");
                    _material.EnableKeyword("_ALPHABLEND_ON");
                    _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    _material.renderQueue = 3000;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, transparency);

            return true;
        }
    }
}