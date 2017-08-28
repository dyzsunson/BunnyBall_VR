using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_AI : RabbitAI {

    float m_min = 0.5f;
    float m_max = 0.8f;

    protected override void Start() {
        base.Start();
        m_y_rotateRange = 0.0f;
        m_x_rotateRange = 0.0f;
        m_fire_wait_minTime = 3.0f;
        m_fire_wait_maxTime = 4.0f;

        m_rotate_wait_minTime = m_rotate_wait_maxTime = 1.0f;
        Invoke("SpeedUp", 20.0f);
    }

    void SpeedUp() {
        if (m_fire_wait_minTime > m_min)
            m_fire_wait_minTime = Mathf.Max(m_min, m_fire_wait_minTime * 0.6f);
        if (m_fire_wait_maxTime > m_max)
            m_fire_wait_maxTime = Mathf.Max(m_max, m_fire_wait_maxTime * 0.6f);
        Invoke("SpeedUp", 10.0f);
    }
}
