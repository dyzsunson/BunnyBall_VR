using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour, Game_Process_Interface{
    // Timer
    public float gameTime;
    float m_time;

    public float TimeLeft {
        get {
            return m_time;
        }
    }

    public Text TimeText;
    public Text WaitTimeText;

    public Text TimeTextVR;
    public Text WaitTimeTextVR;

    // Scene Controll
    public enum SceneState {
        Preparing = 0,
        Waiting = 1,
        Running = 2,
        EndingBuffer = 3,
        End = 3
    }

    private SceneState m_state;

    public SceneState Current_State {
        get {
            return this.m_state;
        }
    }
    private float m_waitTime = 10.0f;
    public float ReadyWaitTime {
        get {
            return this.m_waitTime;
        }
    }

    private bool is_vr_ready = false;
    private bool is_pc_ready = false;

    // Scene Object
    public Level[] levelList;
    static int s_currentLevel = 0;

    public GameObject TouchObj;
    public GameObject LeftTouchObj;
    public GameObject RightTouchObj;
    public GameObject RoamingCameraObj;

    // UI Menu
    public UI_Controller ui_controller;

    public static SceneController context;

    public static Level Level_Current {
        get {
            return context.levelList[s_currentLevel];
        }
    }

    public static EllicControlller Ellic_Current {
        get {
            return Level_Current.g_ellic;
        }
    }

    public static RabbitAI AI_Current {
        get {
            return Level_Current.AI;
        }
    }

    private void Awake() {
        context = this;
    }



    // Use this for initialization
    void Start () {
        m_state = SceneState.Preparing;
        s_currentLevel = NotChanged.context.Level_Current;
        LevelChange(0);

        if (NotChanged.context.is_auto_start == true) {
            if (NotChanged.context.is_single_player == true)
                Start_SinglePlayer();
            else
                Start_MultiPlayer();
        }
        NotChanged.context.is_auto_start = false;
     }
	
	// Update is called once per frame
	void Update () {
        EventSystem.current.SetSelectedGameObject(null);

        if (m_state == SceneState.Running) {
            m_time -= Time.deltaTime;
            if (m_time < 0.0f)
                this.GameEndBuffer();
            else {
                TimeText.text = ((int)m_time).ToString();
                TimeTextVR.text = ((int)m_time).ToString();
            }
        }

        if (m_state == SceneState.Waiting) {
            m_waitTime -= Time.deltaTime;
            if (m_waitTime < 0.0f) {
                this.GameStart();
            }
            else if(m_waitTime < 1.0f) {
                if (WaitTimeText.text != "GO !") {
                    WaitTimeText.transform.Find("Sound2").GetComponent<AudioSource>().Play();
                }

                WaitTimeText.text = "GO !";
                WaitTimeTextVR.text = "GO !";
            }
            else if (m_waitTime < 4.0f) {
                if (WaitTimeText.text != ((int)m_waitTime).ToString()) {
                    WaitTimeText.transform.Find("Sound1").GetComponent<AudioSource>().Play();
                }

                WaitTimeText.text = ((int)m_waitTime).ToString();
                WaitTimeTextVR.text = ((int)m_waitTime).ToString();
            }
        }
	}

    #region public scene controll function
    public void ResetCamera() {
        UnityEngine.VR.InputTracking.Recenter();
    }

    public void PC_Ready() {
        if (m_state == SceneState.Preparing) {
            is_pc_ready = true;
            this.ui_controller.PC_Ready();

            if (is_vr_ready) {
                this.Start_MultiPlayer();
            }
        }
    }

    public void VR_Ready() {
        if (m_state == SceneState.Preparing) {
            is_vr_ready = true;
            this.ui_controller.VR_Ready();

            if (is_pc_ready) {
                this.Start_MultiPlayer();
            }
        }
    }

    public void VR_Ready_Change() {
        if (m_state == SceneState.Preparing) {
            if (is_vr_ready)
                this.VR_Cancel();
            else
                this.VR_Ready();
        }
    }

    public void PC_Cancel() {
        if (m_state == SceneState.Preparing) {
            is_pc_ready = false;
            this.ui_controller.PC_Cancel();
        }
    }

    public void VR_Cancel() {
        if (m_state == SceneState.Preparing) {
            is_vr_ready = false;
            this.ui_controller.VR_Cancel();
        }
    }

    public void Start_SinglePlayer() {
        if (m_state == SceneState.Preparing) {
            InputCtrl.context.Is_AI_Ctrl = true;
            GameReady();
        }
    }

    public void Start_MultiPlayer() {
        if (m_state == SceneState.Preparing) {
            InputCtrl.context.Is_AI_Ctrl = false;
            GameReady();
        }
    }

    public void GameQuit() {
        Application.Quit();
    }

    public void ReStartScene() {
        SceneManager.LoadScene(0);
    }

    public void RestartGame(bool _is_single_player) {
        NotChanged.context.is_auto_start = true;
        NotChanged.context.is_single_player = _is_single_player;
        ReStartScene();
    }

    public void ShowTutorial() {
        if (m_state == SceneState.Preparing) {
            this.ui_controller.ShowTutorial();
        }
    }

    public void HideTutorial() {
        if (m_state == SceneState.Preparing) {
            this.ui_controller.HideTutorial();
        }
    }

    public void NextLevel() {
        if (m_state == SceneState.Preparing) {
            this.LevelChange(1);
        }
    }

    public void LastLevel() {
        if (m_state == SceneState.Preparing) {
            this.LevelChange(-1);
        }
    }

    public void ShowTouchObj() {
        TouchObj.SetActive(true);
        LeftTouchObj.SetActive(true);
        RightTouchObj.SetActive(true);

        this.levelList[s_currentLevel].HandObj.SetActive(false);
    }

    public void ShowGameHandObj() {
        TouchObj.SetActive(false);
        LeftTouchObj.SetActive(false);
        RightTouchObj.SetActive(false);

        this.levelList[s_currentLevel].HandObj.SetActive(true);
    }

    public void ShowTouchObj(float _time) {
        Invoke("ShowTouchObj", _time);
    }

    #endregion


    #region Game_Process_Interface
    public void GameReady() {
        m_time = gameTime;
        m_state = SceneState.Waiting;

        this.ShowGameHandObj();

        this.ui_controller.GameReady();
        RoamingCameraObj.SetActive(false);

        this.levelList[s_currentLevel].GameReady();
        ResetCamera();
    }

    public void GameStart() {
        TimeText.gameObject.SetActive(true);
        TimeText.text = ((int)m_time).ToString();

        TimeTextVR.transform.parent.gameObject.SetActive(true);
        TimeTextVR.text = ((int)m_time).ToString();

        m_state = SceneState.Running;

        WaitTimeText.transform.parent.gameObject.SetActive(false);
        WaitTimeTextVR.transform.parent.gameObject.SetActive(false);

        this.levelList[s_currentLevel].GameStart();

        if (InputCtrl.context.Is_AI_Ctrl)
            SceneController.AI_Current.GameStart();
    }

    public void GameEndBuffer() {
        m_state = SceneState.EndingBuffer;
        this.levelList[s_currentLevel].GameEndBuffer();

        if (InputCtrl.context.Is_AI_Ctrl)
            SceneController.AI_Current.GameEndingBuffer();

        this.ui_controller.GameEndingBuffer();

        Invoke("GameEnd", this.levelList[s_currentLevel].EndingBufferTime);
    }

    public void GameEnd() {
        m_state = SceneState.End;
        this.levelList[s_currentLevel].GameEnd();

        if (InputCtrl.context.Is_AI_Ctrl)
            SceneController.AI_Current.GameEnd();

        this.ShowTouchObj(2.0f);

        this.ui_controller.GameEnd();

        // score
        ScoreCalculate();
        this.GetComponent<AudioSource>().Play();
    }
    #endregion 

    #region private support functions
    void ScoreCalculate() {
        SceneController.Level_Current.GetComponent<ScoreCalculation>().Calculate();
    }

    private void LevelChange(int _offset) {
        int previous_level = s_currentLevel;

        levelList[s_currentLevel].gameObject.SetActive(false);
        s_currentLevel = (s_currentLevel + _offset + levelList.Length) % levelList.Length;
        levelList[s_currentLevel].gameObject.SetActive(true);

        NotChanged.context.Level_Current = s_currentLevel;

        this.ui_controller.LevelChange(levelList[previous_level], levelList[s_currentLevel]);
    }
    #endregion 
}

