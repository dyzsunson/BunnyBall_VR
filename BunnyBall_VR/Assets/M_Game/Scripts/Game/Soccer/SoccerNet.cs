using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerNet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Bullet") {
            // this.GetComponent<AudioSource>().Play();
            // Destroy(collision.gameObject);
            collision.transform.GetComponent<Rigidbody>().velocity *= 0.2f;
            
            
            // Destroy(this.gameObject);
        }
    }
 }
