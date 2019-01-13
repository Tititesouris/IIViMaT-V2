using Interaction.Actors;
using UnityEngine;

namespace Interaction.Reactions.Camera
{
    public class ChangeViewModeReaction : CameraReaction
    {
        [Tooltip("Select which view mode is used by the camera.\n" +
                 "Free View: The camera can move freely" +
                 "Fixed View: The camera is stuck inside a 360 sphere" +
                 "Follow View: The camera can move freely but a 360 sphere follows so that the camera is always inside it")]
        public CameraViewMode.ViewMode viewMode;
    
        [Tooltip("The 360 sphere attached to the camera.")]
        public GameObject videoSphere;

        protected override bool React(Actor actor, RaycastHit? hit)
        {
            if (viewMode != CameraViewMode.ViewMode.FreeView && videoSphere == null)
                return false;
            GetComponent<CameraViewMode>().SetViewMode(viewMode, videoSphere);
            return true;
        }
    }
}