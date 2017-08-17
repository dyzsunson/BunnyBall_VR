using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerScore : MonoBehaviour {
    public ScoreBoard Board_Prefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Soccer") {
            if (other.GetComponent<SoccerBall>().is_in)
                return;

            other.GetComponent<SoccerBall>().is_in = true;
            this.GetComponent<AudioSource>().Play();

            SceneController.Level_Current.GetComponent<Soccer_ScoreCal>().BallIn();

            ScoreBoard score_board = Instantiate(Board_Prefab);
            score_board.transform.position = other.transform.position;
        }
    }
}
