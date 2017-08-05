using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Die", 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Die() {
        Destroy(this.gameObject);
    }
}
