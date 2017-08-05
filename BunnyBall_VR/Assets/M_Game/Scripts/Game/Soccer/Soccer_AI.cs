﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soccer_AI : RabbitAI {
    protected override void Start() {
        base.Start();
        m_fire_wait_minTime = 2.0f;
        m_fire_wait_maxTime = 2.5f;

        m_x_rotateRange = 15.0f;
    }

    protected override void Fire() {
        // base.Fire();
        IsPowerButtonHold = true;

        float t;
        if (is_from_file) {
            t = power[m_current];
        }
        else {
            t = 1.2f * GetFireTime();
        }

        float angle = SceneController.Rabbit_Current.transform.rotation.eulerAngles.y;
        if (angle > 180.0f)
            angle -= 360.0f;

        float curveTime = Random.Range(0.0f, 0.5f); // Mathf.Abs(angle) / m_y_rotateRange * 2.0f

        if (angle < -5.0f)
            IsRightHold = true;
        else if (angle > 5.0f)
            IsLeftHold = true;
        else if (Random.Range(0.0f, 1.0f) < 0.5f)
            IsLeftHold = true;
        else
            IsRightHold = true;

        Invoke("CurveEnd", Mathf.Min(curveTime, t));
        // Invoke("PowerEnd", t);

        Invoke("WaitAfterFire", t);
    }

    void PowerEnd() {
        IsPowerButtonHold = false;
    }

    void CurveEnd() {
        IsLeftHold = IsRightHold = false;
    }
}
