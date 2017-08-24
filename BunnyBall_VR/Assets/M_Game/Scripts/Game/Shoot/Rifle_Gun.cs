using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle_Gun : MonoBehaviour {
    public Transform left_hand;
    public Transform right_hand;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        this.transform.position = right_hand.position;
        this.transform.forward = left_hand.position - right_hand.position;
    }
}
