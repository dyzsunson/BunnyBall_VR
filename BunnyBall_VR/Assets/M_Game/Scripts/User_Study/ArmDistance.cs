using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ArmDistance : MonoBehaviour, Game_Process_Interface {
    Vector3 m_right_position;
    Vector3 m_left_position;
    float m_left_distance = 0.0f;
    float m_right_distance = 0.0f;

    bool m_isWorking = false;
    string m_path = "UserStudy/";

    public void GameEnd() {
        if (!Directory.Exists(m_path)) {
            Directory.CreateDirectory(m_path);
        }

        string fileName = "" + IDInput.UserID + "_" + SceneController.Level_Current.LevelName + "_Distance.txt";
        string distanceText = "Left: " + m_left_distance + "\r\nRight: " + m_right_distance;

        if (!File.Exists(m_path + fileName)) {
            File.Create(m_path + fileName).Dispose();
        }

        File.WriteAllText(m_path + fileName, distanceText);
    }

    public void GameEndBuffer() {
        throw new NotImplementedException();
    }

    public void GameReady() {
        throw new NotImplementedException();
    }

    public void GameStart() {
        m_right_position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        m_left_position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);

        m_left_distance = 0.0f;
        m_right_distance = 0.0f;
        m_isWorking = true;
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (m_isWorking) {
            Vector3 t_new_position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            Vector3 t_new_position_left = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);

            m_right_distance+= Vector3.Distance(t_new_position, m_right_position);
            m_left_distance += Vector3.Distance(t_new_position_left, m_left_position);

            m_right_position = t_new_position;
            m_left_position = t_new_position_left;
        }
        // print(m_distance_total);
    }
}
