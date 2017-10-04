using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAI : RabbitAI {
    float m_min = 0.3f;
    float m_max = 0.5f;

    protected override void Start() {
        base.Start();
        m_fire_wait_minTime = 2.0f;
        m_fire_wait_maxTime = 3.0f;
        m_fire_min = 0.35f;
        m_fire_max = 0.78f;
        m_y_rotateRange = 7.0f;
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
