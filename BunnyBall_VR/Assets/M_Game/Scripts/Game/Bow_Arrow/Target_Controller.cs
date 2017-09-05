using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Controller : MonoBehaviour, Game_Process_Interface {
    enum Target_State {
        WAITING,
        FLASHING,
        STATE1,
        STATE2,
        STATE3,
        STATE4
    }

    Target_State m_state = Target_State.WAITING;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameReady() {
        m_state = Target_State.FLASHING;
    }

    public void GameStart() {

    }

    public void GameEnd() {

    }
}
