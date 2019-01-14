using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class CameraViewMode : MonoBehaviour
{
    public enum ViewMode
    {
        FreeView,
        FixedView,
        FollowView
    }

    public ViewMode viewMode = ViewMode.FreeView;

    public GameObject target;

    private void Awake()
    {
        if (viewMode != ViewMode.FreeView && target == null)
        {
            EditorUtility.DisplayDialog("Error", "Camera View Mode is missing valid target:\n" +
                "View Mode: " + viewMode + "\n" +
                "Target: " + target, "Ok");
            EditorApplication.isPlaying = false;
        }
    }

    private void Update()
    {
            if (target != null)
        {
            var camPos = Vector3.zero;
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<Camera>() != null)
                {
                    camPos = child.localPosition;
                    break;
                }

            }
            switch (viewMode)
            {
                case ViewMode.FreeView:
                    break;
                case ViewMode.FixedView:
                    var destination = target.transform.position;
                    destination.y -= 1.75f;
                    destination.x -= camPos.x;
                    destination.z -= camPos.z;
                    transform.position = destination;
                    break;
                case ViewMode.FollowView:
                    destination = transform.position;
                    destination.y += 1.75f;
                    destination.x += camPos.x;
                    destination.z += camPos.z;
                    target.transform.position = destination;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void SetViewMode(ViewMode viewMode, GameObject target)
    {
        this.viewMode = viewMode;
        this.target = target;
    }
}