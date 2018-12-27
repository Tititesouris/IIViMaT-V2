using Interaction.Reactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interaction.Actors;
using System;

public class ChangeViewReaction : Reaction {

    [Tooltip("Select free view or fixed view")]
    public bool freeView;

    //[Tooltip("If it is not free view, it is possble that the people can move with the video")]
    //public bool followView;

    protected override bool React(Actor actor, RaycastHit? hit) {
        if (actor is CameraActor) {
            actor.GetComponent<ViewMode>().changeView(freeView, gameObject);
            return true;
        }
        return false;
    }
}
