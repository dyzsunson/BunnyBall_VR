using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAI : RabbitAI {
    protected override void Start() {
        base.Start();
        m_fire_min = 0.4f;
        m_x_rotateRange = 8.0f;
    }
}
