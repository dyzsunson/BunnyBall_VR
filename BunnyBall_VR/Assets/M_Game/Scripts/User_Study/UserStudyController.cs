using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStudyController : MonoBehaviour, Game_Process_Interface {
    public ArmDistance g_armDistance;

    public void GameEnd() {
        g_armDistance.GameEnd();
    }

    public void GameEndBuffer() {
    }

    public void GameReady() {
    }

    public void GameStart() {
        g_armDistance.GameStart();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
