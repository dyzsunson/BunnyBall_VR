using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    public Rabbit rabbit;
    public GameObject GoalObj;
    public RabbitAI AI;
    public GameObject HandObj;
    public GameObject TutorialCanvas;
    public GameObject TutorialObj;

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

        if (TutorialObj != null) {
            TutorialObj.SetActive(true);
            TutorialObj.GetComponent<FadeInOut>().FadeIn(1.0f);
        }
        Invoke("HideTutorial", SceneController.context.ReadyWaitTime - 5.0f);
    }

    public void GameStart() {
        rabbit.GameStart();

        if (TutorialObj != null)
            TutorialObj.SetActive(false);
    }

    void ShowTutorial() {
        
    }

    void HideTutorial() {
        TutorialObj.GetComponent<FadeInOut>().FadeOut(2.0f);
        Transform obj_3d = TutorialObj.transform.Find("3D_OBJ");
        if (obj_3d != null)
            obj_3d.gameObject.SetActive(false);
    }

    public void GameEnd() {
        // HandObj.SetActive(false);
        rabbit.GameEnd();
    }
}
