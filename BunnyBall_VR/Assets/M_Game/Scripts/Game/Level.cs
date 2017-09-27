using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour, Game_Process_Interface{
    public EllicControlller g_ellic;
    public GameObject g_goal;
    public RabbitAI AI;
    public GameObject HandObj;
    public GameObject TutorialObj;

    public Sprite LevelIcon;
    public string LevelName;
    public string LevelDescription;

    public float EndingBufferTime = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

#region Game_Process_Interface
    public void GameReady() {
        // HandObj.SetActive(true);
        g_ellic.GameReady();

        if (TutorialObj != null) {
            TutorialObj.SetActive(true);
            TutorialObj.GetComponent<FadeInOut>().FadeIn(1.0f);
        }
        Invoke("HideTutorial", SceneController.context.ReadyWaitTime - 5.0f);

        if (g_goal != null)
            g_goal.GetComponent<Game_Process_Interface>().GameReady();
    }

    public void GameStart() {
        g_ellic.GameStart();

        if (TutorialObj != null)
            TutorialObj.SetActive(false);

        if (g_goal != null)
            g_goal.GetComponent<Game_Process_Interface>().GameStart();
    }

    public void GameEndBuffer() {
        g_ellic.GameEndBuffer();

        if (g_goal != null)
            g_goal.GetComponent<Game_Process_Interface>().GameEndBuffer();
    }

    public void GameEnd() {
        // HandObj.SetActive(false);
        g_ellic.GameEnd();

        if (g_goal != null)
            g_goal.GetComponent<Game_Process_Interface>().GameEnd();
    }
#endregion

    void ShowTutorial() {
        
    }

    void HideTutorial() {
        TutorialObj.GetComponent<FadeInOut>().FadeOut(2.0f);
        Transform obj_3d = TutorialObj.transform.Find("3D_OBJ");
        if (obj_3d != null)
            obj_3d.gameObject.SetActive(false);
    }
}
