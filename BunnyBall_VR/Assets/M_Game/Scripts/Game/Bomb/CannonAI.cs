using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAI : RabbitAI {
    protected override void Start() {
        base.Start();
        m_fire_min = 0.35f;
        m_fire_max = 0.80f;
        m_y_rotateRange = 8.0f;
    }
}
