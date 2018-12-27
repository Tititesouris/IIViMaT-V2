﻿using UnityEngine;

public class FirstPersonControls : MonoBehaviour
{
    private bool focused;

    public float motionSpeed;

    public float rotationSpeed;

    private bool sightHeadingMode;

    public bool fixedView;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        sightHeadingMode = false;
        focused = true;
    }

    private void Update()
    {
        ToggleFocus();

        if (focused)
        {
            ToggleHeadingMode();
            Turn();
            if (!fixedView) {
                Move();
            }
        }
    }

    private void ToggleFocus()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            focused = !focused;
            Cursor.lockState = focused ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    private void ToggleHeadingMode()
    {
        if (Input.GetButtonDown("Fire2"))
            sightHeadingMode = !sightHeadingMode;
    }

    private void Turn()
    {
        gameObject.transform.RotateAround(gameObject.transform.position, Vector3.up,
            Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);
        var rotation = gameObject.transform.eulerAngles;
        rotation.x -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        if (rotation.x < 180)
            rotation.x = Mathf.Min(rotation.x, 90);
        else
            rotation.x = Mathf.Max(rotation.x, 270);
        gameObject.transform.rotation = Quaternion.Euler(rotation);
    }

    private void Move()
    {
        var translation = Vector3.zero;
        translation.x = Input.GetAxis("Horizontal");
        translation.z = Input.GetAxis("Vertical");
        if (sightHeadingMode)
            translation = gameObject.transform.TransformDirection(translation);
        else
            translation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0) * translation;
        gameObject.transform.position += translation * motionSpeed * Time.deltaTime;
    }
}