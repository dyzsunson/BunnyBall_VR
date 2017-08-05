using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour {
    public bool Is_running = false;
    public ShootController m_gun;

    public ShootController ShootCtrl {
        get {
            return m_gun;
        }
    }

    // float m_speed = 0.1f;
    protected float m_rotateSpeed = 0.0f;
    protected float m_max_rotateSpeed = 50.0f;
    protected float m_max_degree = 15.0f;
    protected float m_rotate_a = 250.0f;
    protected float m_degree = 0.0f;

    public float Max_RotateSpeed {
        get {
            return this.m_max_rotateSpeed;
        }
    }

    public float Rotate_A {
        get {
            return this.m_rotate_a;
        }
    }

    // Use this for initialization
    protected virtual void Start() {
        
    }

    // Update is called once per frame
    protected virtual void Update() {
        if (InputCtrl.IsLeftButton) {
            if (m_rotateSpeed > -m_max_rotateSpeed) {
                m_rotateSpeed -= m_rotate_a * Time.deltaTime;
            }
        }
        else if (InputCtrl.IsRightButton) {
            if (m_rotateSpeed < m_max_rotateSpeed) {
                m_rotateSpeed += m_rotate_a * Time.deltaTime;
            }
        }
        else {
            m_rotateSpeed = 0.0f;
        }

        float angle = this.transform.rotation.eulerAngles.y;
        if (angle > 180.0f)
            angle -= 360.0f;

        if (angle < m_max_degree && m_rotateSpeed > 0.0f)
            this.transform.Rotate(this.transform.up, m_rotateSpeed * Time.deltaTime);
        else if (angle > -m_max_degree && m_rotateSpeed < 0.0f)
            this.transform.Rotate(this.transform.up, m_rotateSpeed * Time.deltaTime);
    }

    public void GameReady() {
        Is_running = false;
        foreach (Skill skill in m_gun.skill_array)
            skill.UIObj.SetActive(true);

        m_gun.GameReady();
    }

    public void GameStart() {
        Is_running = true;

        m_gun.GameStart();
    }

    public void GameEnd() {
        Is_running = false;

        m_gun.GameEnd();
    }

    public virtual void Fire() {

    }
}
