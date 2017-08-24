using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour {
    #region public value from outside
    public GameObject StartMenu;
    public GameObject GameMenu;
    public GameObject EndMenu;

    public GameObject StartMenuVR;
    public GameObject GameMenuVR;
    public GameObject EndMenuVR;

    public GameObject TutorialMenu;

    public GameObject Firework;
    #endregion

    #region private value
    GameObject PC_Waiting_obj;
    GameObject VR_Waiting_obj;
    GameObject PC_Ready_obj;
    GameObject VR_Ready_obj;

    GameObject PC_Ready_Button;
    GameObject PC_Cancel_Button;

    GameObject PC_Waiting_obj_VR;
    GameObject VR_Waiting_obj_VR;
    GameObject PC_Ready_obj_VR;
    GameObject VR_Ready_obj_VR;


    Text LevelText;
    Image LevelIcon;

    Text LevelText_VR;
    Text LevelDescription_Text_VR;
    Image LevelIcon_VR;
    #endregion

    private void Awake() {
        PC_Waiting_obj = StartMenu.transform.Find("Prepare/PC_Waiting").gameObject;
        VR_Waiting_obj = StartMenu.transform.Find("Prepare/VR_Waiting").gameObject;
        PC_Ready_obj = StartMenu.transform.Find("Prepare/PC_Ready").gameObject;
        VR_Ready_obj = StartMenu.transform.Find("Prepare/VR_Ready").gameObject;

        PC_Waiting_obj_VR = StartMenuVR.transform.Find("Prepare/PC_Waiting").gameObject;
        VR_Waiting_obj_VR = StartMenuVR.transform.Find("Prepare/VR_Waiting").gameObject;
        PC_Ready_obj_VR = StartMenuVR.transform.Find("Prepare/PC_Ready").gameObject;
        VR_Ready_obj_VR = StartMenuVR.transform.Find("Prepare/VR_Ready").gameObject;

        PC_Ready_Button = StartMenu.transform.Find("Battle").gameObject;
        PC_Cancel_Button = StartMenu.transform.Find("Battle_Cancel").gameObject;

        LevelText = StartMenu.transform.Find("Level/LevelText").GetComponent<Text>();
        LevelIcon = StartMenu.transform.Find("Level/Icon").GetComponent<Image>();

        LevelText_VR = StartMenuVR.transform.Find("Level/LevelText").GetComponent<Text>();
        LevelIcon_VR = StartMenuVR.transform.Find("Level/Icon").GetComponent<Image>();
        LevelDescription_Text_VR = StartMenuVR.transform.Find("Level/Level_Description").GetComponent<Text>();
    }

    // Use this for initialization
    void Start() {
        StartMenu.GetComponent<FadeInOut>().FadeIn(1.0f);
        StartMenuVR.GetComponent<FadeInOut>().FadeIn(1.0f);

        GameMenu.SetActive(false);
        GameMenuVR.SetActive(false);

        EndMenu.SetActive(false);
        EndMenuVR.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

    public void PC_Ready() {
        PC_Waiting_obj.SetActive(false);
        PC_Ready_obj.SetActive(true);

        PC_Waiting_obj_VR.SetActive(false);
        PC_Ready_obj_VR.SetActive(true);

        PC_Ready_Button.SetActive(false);
        PC_Cancel_Button.SetActive(true);
    }

    public void VR_Ready() {
        VR_Waiting_obj.SetActive(false);
        VR_Ready_obj.SetActive(true);

        VR_Waiting_obj_VR.SetActive(false);
        VR_Ready_obj_VR.SetActive(true);
    }

    public void PC_Cancel() {
        PC_Waiting_obj.SetActive(true);
        PC_Ready_obj.SetActive(false);

        PC_Waiting_obj_VR.SetActive(true);
        PC_Ready_obj_VR.SetActive(false);

        PC_Ready_Button.SetActive(true);
        PC_Cancel_Button.SetActive(false);
    }

    public void VR_Cancel() {
        VR_Waiting_obj.SetActive(true);
        VR_Ready_obj.SetActive(false);

        VR_Waiting_obj_VR.SetActive(true);
        VR_Ready_obj_VR.SetActive(false);
    }

    public void GameReady() {
        GameMenu.SetActive(true);
        GameMenu.GetComponent<FadeInOut>().FadeIn(1.0f);

        GameMenuVR.SetActive(true);
        GameMenuVR.GetComponent<FadeInOut>().FadeIn(1.0f);

        StartMenu.SetActive(false);
        StartMenuVR.SetActive(false);
    }

    public void GameStart() {

    }

    public void GameEndingBuffer() {
        GameMenu.SetActive(false);
        GameMenuVR.SetActive(false);
    }

    public void GameEnd() {
        EndMenu.SetActive(true);
        EndMenuVR.SetActive(true);

        Firework.SetActive(true);

        EndMenu.GetComponent<FadeInOut>().FadeIn(1.0f);
        EndMenuVR.GetComponent<FadeInOut>().FadeIn(1.0f);
    }

    public void ShowTutorial() {
        TutorialMenu.SetActive(true);
        TutorialMenu.GetComponent<FadeInOut>().FadeIn(1.0f);
    }

    public void HideTutorial() {
        TutorialMenu.SetActive(false);
    }

    public void LevelChange(Level _previous, Level _level) {
        LevelText.text = LevelText_VR.text = _level.LevelName;
        LevelDescription_Text_VR.text = _level.LevelDescription;

        LevelIcon.sprite = LevelIcon_VR.sprite = _level.LevelIcon;

        /*
        if (_previous != null)
            _previous.TutorialCanvas.SetActive(false);

        _level.TutorialCanvas.SetActive(true);
        */
    }
}
