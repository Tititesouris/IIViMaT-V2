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
            switch (viewMode)
            {
                case ViewMode.FreeView:
                    break;
                case ViewMode.FixedView:
                    transform.position = target.transform.position;
                    break;
                case ViewMode.FollowView:
                    target.transform.position = transform.position;
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