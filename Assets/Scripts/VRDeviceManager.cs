using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRDeviceManager : MonoBehaviour {

    public float motionSpeed;

    public float rotationSpeed;

    private bool _focused;

    private bool _sightHeadingMode;

    private void Start()
    {
        if (!XRDevice.isPresent)
        {
            transform.position = new Vector3(0, 1.70f, 0);
            Cursor.lockState = CursorLockMode.Locked;
            _sightHeadingMode = false;
            _focused = true;
        }
    }

    private void Update()
    {
        if (!XRDevice.isPresent)
        {
            ToggleFocus();

            if (_focused)
            {
                ToggleHeadingMode();
                Turn();
                Move();
            }
        }

    }

    private void ToggleFocus()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _focused = !_focused;
            Cursor.lockState = _focused ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    private void ToggleHeadingMode()
    {
        if (Input.GetButtonDown("Fire2"))
            _sightHeadingMode = !_sightHeadingMode;
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
        if (_sightHeadingMode)
            translation = gameObject.transform.TransformDirection(translation);
        else
            translation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0) * translation;
        gameObject.transform.position += translation * motionSpeed * Time.deltaTime;
    }
}
