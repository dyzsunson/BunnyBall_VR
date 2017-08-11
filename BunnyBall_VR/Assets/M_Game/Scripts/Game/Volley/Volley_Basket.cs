using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volley_Basket : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "VolleyBall") {
            Volley_Ball ball = other.transform.GetComponent<Volley_Ball>();
            if (!ball.Is_in_basket) {
                ball.EnterBasket();
                this.GetComponent<AudioSource>().Play();
            }
        }
    }
}
