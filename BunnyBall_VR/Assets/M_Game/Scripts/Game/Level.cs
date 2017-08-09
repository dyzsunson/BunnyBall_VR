using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    public Rabbit rabbit;
    public GameObject GoalObj;
    public RabbitAI AI;
    public GameObject HandObj;
    public GameObject TutorialObj;

    public Sprite LevelIcon;
    public string LevelName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameReady() {
        // HandObj.SetActive(true);
        rabbit.GameReady();
    }

    public void GameStart() {
        rabbit.GameStart();
    }

    public void GameEnd() {
        // HandObj.SetActive(false);
        rabbit.GameEnd();
    }
}
