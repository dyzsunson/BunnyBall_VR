using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volley_AI : RabbitAI {

	protected override void Start() {
        base.Start();
        m_fire_min = 0.35f;
        m_fire_max = 0.35f;
        m_y_rotateRange = 5.0f;
        m_x_rotateRange = 0.0f;
        m_fire_wait_minTime = m_fire_wait_maxTime = 3.0f;
    }
}
