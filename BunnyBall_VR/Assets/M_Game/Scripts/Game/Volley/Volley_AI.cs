using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volley_AI : RabbitAI {

	protected override void Start() {
        base.Start();
        m_fire_min = 0.5f;
        m_fire_max = 1.0f;
        m_y_rotateRange = 5.0f;
        m_x_rotateRange = 0.0f;
        m_fire_wait_minTime = m_fire_wait_maxTime = 2.0f;
    }

    protected override void Update() {
        base.Update();
        if (SceneController.context.Current_State == SceneController.SceneState.Running) {
            if (SceneController.context.TimeLeft < 3.0f) {
                this.GameEndingBuffer();
            }
        }
    }
}
