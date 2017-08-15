using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volley_Basket : MonoBehaviour {
    public int Basket_ID;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RightBasket() {
        this.transform.parent.Find("Glow").gameObject.SetActive(true);
    }

    public void WrongBasket() {
        this.transform.parent.Find("Glow").gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "VolleyBall") {
            Volley_Ball ball = other.transform.GetComponent<Volley_Ball>();
            if (!ball.Is_in_basket) {
                ball.EnterBasket();

                SceneController.Level_Current.GetComponent<Volley_Score_Cal>().InBasket(this.Basket_ID == ball.Basket_ID);
                this.GetComponent<AudioSource>().Play();
            }
        }
    }
}
