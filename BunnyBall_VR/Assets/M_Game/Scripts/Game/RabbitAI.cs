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
    protected float[] array = { -10.0f, 10.0f, -10.0f, 10.0f, -10.0f, 10.0f, -10.0f, 10.0f, -10.0f, 10.0f, -10.0f, 10.0f, -10.0f, 10.0f, -10.0f, 10.0f };
    protected float[] power = { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
    protected int m_current = 0;

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
    public bool is_from_file = false;

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
        keyDownDictionary = new Dictionary<KeyCode, bool>();
        foreach (KeyCode key in keyList) {
            keyDownDictionary.Add(key, false);
        }

        m_skill_reloading_time = Random.Range(m_min_skill_time, m_max_skill_time);
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

        m_skill_reloading_time = Random.Range(m_min_skill_time, m_max_skill_time);
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

        if (is_from_file) {
            m_current++;
            if (m_current >= array.Length)
                return;
        }

        float ty = RotateAroundY();
        float tx = RotateAroundX();

        Invoke("WaitAfterRotate", Mathf.Max(tx, ty));
    }

    float RotateAroundY() {
        float startDegree = SceneController.Ellic_Current.transform.rotation.eulerAngles.y;
        if (startDegree > 180.0f)
            startDegree -= 360.0f;

        float endDegree = 0.0f;
        if (is_from_file) {
            endDegree = array[m_current];
        }
        else {
            endDegree = Random.Range(-m_y_rotateRange, m_y_rotateRange);
        }
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
        if (is_from_file) {
            endDegree = array[m_current];
        }
        else {
            endDegree = Random.Range(-m_x_rotateRange, m_x_rotateRange);
        }
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
        if (is_from_file) {
            SceneController.Ellic_Current.transform.rotation = Quaternion.Euler(new Vector3(0.0f, array[m_current], 0.0f));
        }

        IsUpHold = IsDownHold = IsLeftHold = IsRightHold = false;

        float t = Random.Range(m_rotate_wait_minTime, m_rotate_wait_maxTime);
        Invoke("Fire", t);
    }
#endregion

#region FIRE
    protected virtual void Fire() {
        IsPowerButtonHold = true;

        float t;
        if (is_from_file) {
            t = power[m_current];
        }
        else {
            t = GetFireTime();
        }

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


        return Random.Range(low, high);
    }

    void WaitAfterFire() {
        IsPowerButtonHold = false;

        float t = Random.Range(m_fire_wait_minTime, m_fire_wait_maxTime);
        Invoke("RotateNext", t);
    }
#endregion

    public void GameStart() {
        isWorking = true;
        m_current = -1;
        RotateNext();
    }

    public void GameEndingBuffer() {
        isWorking = false;
    }

    public void GameEnd() {
        // isWorking = false;
    }
}
