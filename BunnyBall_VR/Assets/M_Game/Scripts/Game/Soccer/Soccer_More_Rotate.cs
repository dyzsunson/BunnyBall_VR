using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soccer_More_Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, this.transform.parent.localRotation.eulerAngles.y, 0.0f));
	}
}
