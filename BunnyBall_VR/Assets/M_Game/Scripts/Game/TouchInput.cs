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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneController.context.Current_State == SceneController.SceneState.Preparing) {
            if (OVRInput.GetUp(OVRInput.RawButton.A)) {
                SceneController.context.VR_Ready_Change();
            }

            if (OVRInput.GetUp(OVRInput.RawButton.B)) {
                SceneController.context.Start_SinglePlayer();
            }

            float x = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;
            if (x > 0.9f && m_state != AxisState.RIGHT) {
                m_state = AxisState.RIGHT;
                SceneController.context.NextLevel();
            }
            else if (x < -0.9f && m_state != AxisState.LEFT) {
                m_state = AxisState.LEFT;
                SceneController.context.LastLevel();
            }

            if (x < 0.2f && x > -0.2f) {
                m_state = AxisState.MIDDLE;
            }
        }
        else if (SceneController.context.Current_State == SceneController.SceneState.Running) {

        }
        else if (SceneController.context.Current_State == SceneController.SceneState.End) {

        }
    }
}
