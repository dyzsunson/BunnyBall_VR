using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<FadeInOut>().FadeOut(2.0f);
        Invoke("End", 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetScore(int _score) {
        foreach (Text text in this.transform.GetComponentsInChildren<Text>()) {
            text.text = "+" + _score;
        }
    }

    void End() {
        Destroy(this.gameObject);
    }
}
