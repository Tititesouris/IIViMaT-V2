using Interaction.Reactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interaction.Actors;
using System;

public class TeleportationReaction : Reaction {

    [Tooltip("The gameObject where the teleportation go. If it is null, we use the parametre position")]
    public GameObject positionGameObject;

    [Tooltip("The position x, y, z")]
    public Vector3 position;

    [Tooltip("The telaporation is direct or fast")]
    public bool isDirect;

    public float speed;

    protected override bool React(Actor actor, RaycastHit? hit) {
        if(positionGameObject != null) {
            position = positionGameObject.transform.position;
        }

        if (isDirect) {
            actor.transform.position = position;
        } else {
            IEnumerator translate = TranslateToPosition(actor);
            StartCoroutine(translate);
        }

        return true;
    }

    IEnumerator TranslateToPosition(Actor actor) {
        float step = (speed / (actor.transform.position - position).magnitude) * Time.deltaTime;
        float t = 0;
        while (t <= 1.0f) {
            t += step;
            actor.transform.position = Vector3.MoveTowards(actor.transform.position, position, t);
            yield return null; 
        }
        actor.transform.position = position;
    }
}
