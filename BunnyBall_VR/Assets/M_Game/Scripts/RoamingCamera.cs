﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 3.0f * Time.deltaTime);
	}
}
