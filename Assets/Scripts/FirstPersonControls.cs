using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPersonControls : MonoBehaviour {

    public float motionSpeed;

    public float rotationSpeed;

    private bool sightHeadingMode;

    private bool focused;
    
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        this.sightHeadingMode = false;
        this.focused = true;
    }
	
	void Update ()
    {
        ToggleFocus();

        if (this.focused)
        {
            ToggleHeadingMode();
            Turn();
            Move();
        }
    }

    private void ToggleFocus()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this.focused = !this.focused;
            Cursor.lockState = (this.focused) ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

    private void ToggleHeadingMode()
    {
        if (Input.GetButtonDown("Fire2"))
            this.sightHeadingMode = !this.sightHeadingMode;
    }

    private void Turn()
    {
        this.gameObject.transform.RotateAround(this.gameObject.transform.position, Vector3.up, Input.GetAxis("Mouse X") * this.rotationSpeed * Time.deltaTime);
        Vector3 rotation = this.gameObject.transform.eulerAngles;
        rotation.x -= Input.GetAxis("Mouse Y") * this.rotationSpeed * Time.deltaTime;
        if (rotation.x < 180)
            rotation.x = Mathf.Min(rotation.x, 90);
        else
            rotation.x = Mathf.Max(rotation.x, 270);
        this.gameObject.transform.rotation = Quaternion.Euler(rotation);
    }

    private void Move()
    {
        Vector3 translation = Vector3.zero;
        translation.x = Input.GetAxis("Horizontal");
        translation.z = Input.GetAxis("Vertical");
        if (sightHeadingMode)
            translation = this.gameObject.transform.TransformDirection(translation);
        else
        {
            translation = Quaternion.Euler(0, this.gameObject.transform.eulerAngles.y, 0) * translation;
        }
        this.gameObject.transform.position += translation * this.motionSpeed * Time.deltaTime;
    }
}
