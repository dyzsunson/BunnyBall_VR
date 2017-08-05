using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {
    public OVRInput.Controller controller;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
        this.transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
    }
}
