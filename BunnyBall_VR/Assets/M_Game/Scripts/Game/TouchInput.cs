using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour {
    enum AxisState {
        LEFT,
        RIGHT,
        MIDDLE
    }

    AxisState m_state = AxisState.MIDDLE;
	
    public GameObject BackMenu_VR;
    bool is_backBtn_show = false;

    float r_time = 0.0f;
    float l_time = 0.0f;

    float m_vib_time = 0.2f;
    float m_vid_power = 0.5f;

    // Use this for initialization
    void Start() {
        OVRInput.SetControllerVibration(0.0f, 0.0f, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0.0f, 0.0f, OVRInput.Controller.LTouch);
    }

    // Update is called once per frame
    void Update () {
        if (r_time > 0.0f) {
            r_time -= Time.deltaTime;
            if (r_time <= 0.0f) {
                OVRInput.SetControllerVibration(0.0f, 0.0f, OVRInput.Controller.RTouch);
            }
        }

        if (l_time > 0.0f) {
            l_time -= Time.deltaTime;
            if (l_time <= 0.0f) {
                OVRInput.SetControllerVibration(0.0f, 0.0f, OVRInput.Controller.LTouch);
            }
        }

        if (SceneController.context.Current_State == SceneController.SceneState.Preparing) {
            if (OVRInput.GetUp(OVRInput.RawButton.B)) {
                SceneController.context.VR_Ready_Change();

                this.TouchVibration(OVRInput.Controller.RTouch, m_vib_time, m_vid_power);
            }

            if (OVRInput.GetUp(OVRInput.RawButton.A)) {
                SceneController.context.Start_SinglePlayer();

                this.TouchVibration(OVRInput.Controller.RTouch, m_vib_time, m_vid_power);
            }

            float x = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;
            if (x > 0.9f && m_state != AxisState.RIGHT) {
                m_state = AxisState.RIGHT;
                SceneController.context.NextLevel();

                this.TouchVibration(OVRInput.Controller.RTouch, m_vib_time, m_vid_power);
            }
            else if (x < -0.9f && m_state != AxisState.LEFT) {
                m_state = AxisState.LEFT;
                SceneController.context.LastLevel();

                this.TouchVibration(OVRInput.Controller.RTouch, m_vib_time, m_vid_power);
            }

            if (x < 0.2f && x > -0.2f) {
                m_state = AxisState.MIDDLE;
            }

            if (OVRInput.GetUp(OVRInput.RawButton.Y)) {
                SceneController.context.ResetCamera();

                this.TouchVibration(OVRInput.Controller.LTouch, m_vib_time, m_vid_power);
            }
        }
        else if (SceneController.context.Current_State == SceneController.SceneState.Running 
            || SceneController.context.Current_State == SceneController.SceneState.Waiting) {
            if (OVRInput.GetUp(OVRInput.RawButton.B)) {
                is_backBtn_show = !is_backBtn_show;
                BackMenu_VR.SetActive(is_backBtn_show);

                if (is_backBtn_show) {
                    BackMenu_VR.GetComponent<FadeInOut>().FadeIn(1.0f);
                    SceneController.context.ShowTouchObj();
                }
                else {
                    SceneController.context.ShowGameHandObj();
                }

                this.TouchVibration(OVRInput.Controller.RTouch, m_vib_time, m_vid_power);
            }

            if (OVRInput.GetUp(OVRInput.RawButton.A)) {
                if (is_backBtn_show == true)
                    SceneController.context.ReStartScene();

                this.TouchVibration(OVRInput.Controller.RTouch, m_vib_time, m_vid_power);
            }
        }
        else if (SceneController.context.Current_State == SceneController.SceneState.End) {
            // if (OVRInput.GetUp(OVRInput.RawButton.A)) {
            //     SceneController.context.RestartGame(InputCtrl.context.Is_AI_Ctrl);
            // }

            if (OVRInput.GetUp(OVRInput.RawButton.B)) {
                SceneController.context.ReStartScene();

                this.TouchVibration(OVRInput.Controller.RTouch, m_vib_time, m_vid_power);
            }
        }
    }

    void TouchVibration(OVRInput.Controller _controller, float _time, float _power) {
        OVRInput.SetControllerVibration(_power, _power, _controller);
        if (_controller == OVRInput.Controller.LTouch)
            l_time = _time;
        else if (_controller == OVRInput.Controller.RTouch)
            r_time = 0.2f;
    }
}
