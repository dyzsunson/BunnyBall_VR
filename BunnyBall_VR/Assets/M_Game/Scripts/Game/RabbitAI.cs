using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitAI : MonoBehaviour {
    // button
    public bool IsLeftHold;
    public bool IsRightHold;
    public bool IsUpHold;
    public bool IsDownHold;
    public bool IsPowerButtonHold;
    public bool IsPowerButtonUp;

    // from file
    public bool is_from_file = false;

    // rotate variable
    protected float m_rotate_a = 250.0f;
    protected float m_max_speed = 50.0f;

    protected float m_y_rotateRange = 10.0f;
    protected float m_x_rotateRange = 5.0f;

    // fire variable
    protected float m_fire_min = 0.3f;
    protected float m_fire_max = 0.75f;

    // private float m_y_rotate_a = 250.0f;
    // private float m_y_max_speed = 50.0f;

    protected bool isWorking = false;

    // reloadTime
    protected float m_fire_wait_minTime = 0.1f;
    protected float m_fire_wait_maxTime = 0.3f;
    protected float m_rotate_wait_minTime = 0.1f;
    protected float m_rotate_wait_maxTime = 0.2f;

    bool is_last_powerButtonHold = false;

    public KeyCode[] keyList;
    private Dictionary<KeyCode, bool> keyDownDictionary;
    private int m_current_skill = -1;
    private KeyCode m_lastKey = KeyCode.F1;
    private float m_min_skill_time = 10.0f;
    private float m_max_skill_time = 15.0f;
    float m_skill_reloading_time;

    // Use this for initialization
    protected virtual void Start() {
       
    }

    // Update is called once per frame
    protected virtual void Update() {
        if (!is_last_powerButtonHold && IsPowerButtonHold) {
            is_last_powerButtonHold = true;
        }

        if (is_last_powerButtonHold && !IsPowerButtonHold) {
            is_last_powerButtonHold = false;
            IsPowerButtonUp = true;
        }
        else {
            IsPowerButtonUp = false;
        }

        if (isWorking && keyList.Length > 0) {
            if (m_lastKey != KeyCode.F1) {
                keyDownDictionary[m_lastKey] = false;
                m_lastKey = KeyCode.F1;
            }

            m_skill_reloading_time -= Time.deltaTime;
            if (m_skill_reloading_time < 0.0f) {
                UseSkill();
            }
        }
    }

    void UseSkill() {
        m_current_skill++;
        m_current_skill %= keyList.Length;

        keyDownDictionary[keyList[m_current_skill]] = true;
        m_lastKey = keyList[m_current_skill];

       
        m_skill_reloading_time = MRandom.context.GetNextRandom(m_min_skill_time, m_max_skill_time, is_from_file);
    }

    public bool IsHotKeyDown(KeyCode key) {
        if (!keyDownDictionary.ContainsKey(key))
            return false;
        else
            return keyDownDictionary[key];
    }

#region rotation
    void RotateNext() {
        if (!isWorking)
            return;

        float ty = RotateAroundY();
        float tx = RotateAroundX();

        Invoke("WaitAfterRotate", Mathf.Max(tx, ty));
    }

    float RotateAroundY() {
        float startDegree = SceneController.Ellic_Current.transform.rotation.eulerAngles.y;
        if (startDegree > 180.0f)
            startDegree -= 360.0f;

        float endDegree = 0.0f;

        endDegree = MRandom.context.GetNextRandom(-m_y_rotateRange, m_y_rotateRange, is_from_file);
        
        float t = TimeRotateFromTo(startDegree, endDegree, m_max_speed, m_rotate_a);
        if (endDegree > startDegree)
            IsRightHold = true;
        else
            IsLeftHold = true;

        Invoke("RotateYEnd", t);

        return t;
    }

    float RotateAroundX() {
        float startDegree = SceneController.Ellic_Current.ShootCtrl.GunBodyTransform.rotation.eulerAngles.x;
        if (startDegree > 180.0f)
            startDegree -= 360.0f;

        float endDegree = 0.0f;
        
        endDegree = MRandom.context.GetNextRandom(-m_x_rotateRange, m_x_rotateRange, is_from_file);

        float t = TimeRotateFromTo(startDegree, endDegree, m_max_speed, m_rotate_a);

        if (endDegree < startDegree)
            IsUpHold = true;
        else
            IsDownHold = true;

        Invoke("RotateXEnd", t);

        return t;
    }

    float TimeRotateFromTo(float _start, float _end, float _speed, float _a) {
        float t = 0.0f;

        float a_length = 0.5f * m_max_speed * m_max_speed / m_rotate_a;

        float dis = Mathf.Abs(_end - _start);
        if (dis < a_length) {
            t = Mathf.Sqrt(dis * 2.0f / m_rotate_a);
        }
        else {
            t = (dis - a_length) / m_max_speed + m_max_speed / m_rotate_a;
        }

        return t;
    }

    void RotateXEnd() {
        IsUpHold = IsDownHold = false;
    }

    void RotateYEnd() {       
        IsLeftHold = IsRightHold = false;
    }

    void WaitAfterRotate() {
        IsUpHold = IsDownHold = IsLeftHold = IsRightHold = false;

        float t = MRandom.context.GetNextRandom(m_rotate_wait_minTime, m_rotate_wait_maxTime, is_from_file);
        Invoke("Fire", t);
    }
#endregion

#region FIRE
    protected virtual void Fire() {
        IsPowerButtonHold = true;

        float t;
        t = GetFireTime();

        Invoke("WaitAfterFire", t);
    }

    protected virtual float GetFireTime() {
        float degree = SceneController.Ellic_Current.ShootCtrl.GunBodyTransform.rotation.eulerAngles.x;
        if (degree > 180.0f)
            degree -= 360.0f;

        if (degree > 5.0f)
            degree = 5.0f;
        if (degree < -5.0f)
            degree = -5.0f;

        float low = m_fire_min;
        float high = m_fire_max;

        float diff = m_fire_max - m_fire_min;
        if (degree < 0.0f)
            high -= diff * (-degree) / 5.0f;
        else
            low += 0.5f * diff * (degree) / 5.0f;


        return MRandom.context.GetNextRandom(low, high, is_from_file);
    }

    void WaitAfterFire() {
        IsPowerButtonHold = false;

        float t = MRandom.context.GetNextRandom(m_fire_wait_minTime, m_fire_wait_maxTime, is_from_file);
        Invoke("RotateNext", t);
    }
#endregion

    public void GameStart() {
        MRandom.context.ResetFile("random_data.txt");

        keyDownDictionary = new Dictionary<KeyCode, bool>();
        foreach (KeyCode key in keyList) {
            keyDownDictionary.Add(key, false);
        }

        m_skill_reloading_time = MRandom.context.GetNextRandom(m_min_skill_time, m_max_skill_time, is_from_file);
            
        isWorking = true;
        RotateNext();
    }

    public void GameEndingBuffer() {
        isWorking = false;
    }

    public void GameEnd() {
        // isWorking = false;
    }
}
