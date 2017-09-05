using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Controller : MonoBehaviour {
    enum AxisState {
        LEFT,
        RIGHT,
        MIDDLE
    }

    AxisState m_state = AxisState.MIDDLE;

    float r_time = 0.0f;
    float l_time = 0.0f;

    float m_vib_time = 0.2f;
    float m_vid_power = 0.5f;

    int m_current_weapon = 0;

    public GameObject[] p_weapon_array;
    public GameObject[] p_weapon_ui_array;

    // Use this for initialization
    void Start () {
        p_weapon_ui_array[m_current_weapon].SetActive(true);
        p_weapon_array[m_current_weapon].SetActive(true);
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

        float x = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).x;
        if (x > 0.9f && m_state != AxisState.RIGHT) {
            m_state = AxisState.RIGHT;

            Weapon_Change(1);

            this.TouchVibration(OVRInput.Controller.RTouch, m_vib_time, m_vid_power);
        }
        else if (x < -0.9f && m_state != AxisState.LEFT) {
            m_state = AxisState.LEFT;

            Weapon_Change(-1);

            this.TouchVibration(OVRInput.Controller.RTouch, m_vib_time, m_vid_power);
        }

        if (x < 0.2f && x > -0.2f) {
            m_state = AxisState.MIDDLE;
        }
    }

    void Weapon_Change(int _offset) {
        p_weapon_array[m_current_weapon].SetActive(false);
        p_weapon_ui_array[m_current_weapon].SetActive(false);

        m_current_weapon = (m_current_weapon + _offset + p_weapon_array.Length) % p_weapon_array.Length;

        p_weapon_array[m_current_weapon].SetActive(true);
        p_weapon_ui_array[m_current_weapon].SetActive(true);
    }

    void TouchVibration(OVRInput.Controller _controller, float _time, float _power) {
        OVRInput.SetControllerVibration(_power, _power, _controller);
        if (_controller == OVRInput.Controller.LTouch)
            l_time = _time;
        else if (_controller == OVRInput.Controller.RTouch)
            r_time = 0.2f;
    }
}
