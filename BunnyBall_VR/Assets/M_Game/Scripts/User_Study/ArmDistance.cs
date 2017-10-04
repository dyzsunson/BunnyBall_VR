using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmDistance : MonoBehaviour {
    Vector3 m_right_position;
    Vector3 m_left_position;
    float m_distance_total = 0.0f;
	// Use this for initialization
	void Start () {
        m_right_position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        m_left_position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 t_new_position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Vector3 t_new_position_left = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);

        m_distance_total += Vector3.Distance(t_new_position, m_right_position);
        m_distance_total += Vector3.Distance(t_new_position_left, m_left_position);

        m_right_position = t_new_position;
        m_left_position = t_new_position_left;

        print(m_distance_total);
    }
}
