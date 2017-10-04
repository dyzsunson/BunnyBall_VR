using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soccer_AI : RabbitAI {
    protected override void Start() {
        base.Start();
        m_fire_wait_minTime = 1.0f;
        m_fire_wait_maxTime = 1.5f;

        m_fire_min = 0.35f;

        m_y_rotateRange = 12.0f;
        m_x_rotateRange = 10.0f;
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

        float angle = SceneController.Ellic_Current.transform.rotation.eulerAngles.y;
        if (angle > 180.0f)
            angle -= 360.0f;

        float t_min = 0.0f, t_max = 0.5f;
        
        if (angle < -5.0f)
            IsRightHold = true;
        else if (angle > 5.0f)
            IsLeftHold = true;
        else if (Random.Range(0.0f, 1.0f) < 0.5f)
            IsLeftHold = true;
        else
            IsRightHold = true;

        if (angle > 7.5f || angle < -7.5f)
            t_min = 0.3f;
        else if ((angle < 0.0f && IsLeftHold == true) || (angle > 0.0f && IsRightHold == true))
            t_max = 0.2f;

        float curveTime = 0.85f * Random.Range(t_min, t_max); // Mathf.Abs(angle) / m_y_rotateRange * 2.0f

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
