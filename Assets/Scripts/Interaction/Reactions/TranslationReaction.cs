using Interaction.Reactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interaction.Actors;
using System;

public class TranslationReaction : Reaction {

    [Tooltip("The time during the object translate.")]
    public float time;

    [Tooltip("Translation for left and right.")]
    public float translationX;

    [Tooltip("Translation for up and down.")]
    public float translationY;

    [Tooltip("Translation for in front and behind.")]
    public float translationZ;

    protected override bool React(Actor actor, RaycastHit? hit) {
        StartCoroutine("TranslateCoroutine");
        return true;
    }

    IEnumerator TranslateCoroutine() {
        for (int i = 0; i < time * 60; i++) {
            transform.Translate(translationX * Time.deltaTime, translationY * Time.deltaTime, translationZ * Time.deltaTime);
            yield return null;
        }
    }

}
