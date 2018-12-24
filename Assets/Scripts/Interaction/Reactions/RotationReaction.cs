using Interaction.Reactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interaction.Actors;
using System;

public class RotationReaction : Reaction {

    [Tooltip("The time during the object rotate.")]
    public float time;

    public enum Horizontal { None, Left, Right };
    public enum Vertical { None, Up, Down };

    public Horizontal horizontalRotation = Horizontal.None;

    public Vertical verticalRotation = Vertical.None;

    protected override bool React(Actor actor, RaycastHit? hit) {
        float rotateX = 0;
        float rotateY = 0;

        if(horizontalRotation == Horizontal.Left) {
            rotateX = transform.rotation.x + 1f;
        } else if (horizontalRotation == Horizontal.Right) {
            rotateX = transform.rotation.x - 1f;
        }

        if (verticalRotation == Vertical.Up) {
            rotateY = transform.rotation.y + 1f;
        } else if (verticalRotation == Vertical.Down) {
            rotateY = transform.rotation.y - 1f;
        }

        IEnumerator r = RotateCoroutine(rotateX, rotateY);
        StartCoroutine(r);
        return true;
    }

    IEnumerator RotateCoroutine(float rotateX, float rotateY) {
        for (int i = 0; i < time*60; i++) {
            transform.Rotate(rotateY, rotateX, 0);
            yield return null;
        }
    }

}
