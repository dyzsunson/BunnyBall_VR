using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    public Rabbit rabbit;
    public GameObject GoalObj;
    public RabbitAI AI;
    public GameObject HandObj;
    public GameObject TutorialObj;
    public GameObject GameTutorial;

    public Sprite LevelIcon;
    public string LevelName;
    public string LevelDescription;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameReady() {
        // HandObj.SetActive(true);
        rabbit.GameReady();

        if (GameTutorial != null) {
            GameTutorial.SetActive(true);
            GameTutorial.GetComponent<FadeInOut>().FadeIn(1.0f);
        }
        Invoke("HideTutorial", SceneController.context.ReadyWaitTime - 5.0f);
    }

    public void GameStart() {
        rabbit.GameStart();

        if (GameTutorial != null)
            GameTutorial.SetActive(false);
    }

    void ShowTutorial() {
        
    }

    void HideTutorial() {
        GameTutorial.GetComponent<FadeInOut>().FadeOut(2.0f);
    }

    public void GameEnd() {
        // HandObj.SetActive(false);
        rabbit.GameEnd();
    }
}
