using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeet_AI : RabbitAI {
    protected override void Start() {
        base.Start();
        m_fire_min = 0.5f;
        m_fire_max = 1.0f;
        m_y_rotateRange = 25.0f;
        m_x_rotateRange = 3.0f;
        m_fire_wait_minTime = 0.35f;
        m_fire_wait_maxTime = 0.5f;
    }

    protected override void Update() {
        base.Update();
        if (SceneController.context.Current_State == SceneController.SceneState.Running) {
            if (SceneController.context.TimeLeft < 2.0f) {
                this.GameEndingBuffer();
            }
        }
    }
}
