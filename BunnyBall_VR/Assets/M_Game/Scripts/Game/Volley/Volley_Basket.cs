using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volley_Basket : MonoBehaviour {
    public int Basket_ID;
    public ScoreBoard Board_Prefab;

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

                int score = 1;
                if (this.Basket_ID == ball.Basket_ID)
                    score = 2;
                if (ball.is_moneyBall == true)
                    score *= 2;

                SceneController.Level_Current.GetComponent<Volley_Score_Cal>().InBasket(score);
                this.GetComponent<AudioSource>().Play();

                ScoreBoard score_board = Instantiate(Board_Prefab);
                score_board.transform.position = other.transform.position + new Vector3(0.0f, 0.5f, 0.0f);

                score_board.SetScore(score);
            }
        }
    }
}
