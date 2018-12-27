using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMode : MonoBehaviour {

    private bool freeView = true;

    //TODO est ce util? ca cause des complications: on ne sait plus tourner la tete dans la video + ajout de parent
    //private bool followView = false; 

    [Tooltip("The video that the people can not leave")]
    private GameObject objectToFixed;


	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
        /*if(objectToFixed != null && !followView) {
            objectToFixed.transform.parent = null;
        }*/
        if (freeView) {
            GetComponent<FirstPersonControls>().fixedView = false; //TODO en fonction de si on est avec la vr ou pas
        } else {
            /*if (followView) {
                GetComponent<FirstPersonControls>().fixedView = false;
                objectToFixed.transform.position = transform.position;
                objectToFixed.transform.parent = transform;
            } else {*/
                GetComponent<FirstPersonControls>().fixedView = true;
                transform.position = objectToFixed.transform.position;
            //}
        }
	}

    public void changeView(bool freeView, GameObject objectToFixed) {
        this.freeView = freeView;
        //this.followView = followView;
        this.objectToFixed = objectToFixed;
    }
}
