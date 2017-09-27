using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllicControlller : MonoBehaviour, Game_Process_Interface {
    public bool g_is_running = false;
    public ShootController g_shooter;

    public ShootController ShootCtrl {
        get {
            return g_shooter;
        }
    }

    // float m_speed = 0.1f;
    protected float m_rotateSpeed = 0.0f;
    protected float m_max_rotateSpeed = 50.0f;
    protected float m_max_degree = 15.0f;
    protected float m_rotate_a = 250.0f;
    protected float m_degree = 0.0f;

    protected Animator m_animator;

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

    protected virtual void Awake() {
        if (this.transform.Find("Ellic") != null)
            m_animator = this.transform.Find("Ellic").GetComponent<Animator>();
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

#region Game_Process_Interface
    public void GameReady() {
        g_is_running = false;

        if (g_shooter != null)
            g_shooter.GameReady();
    }

    public void GameStart() {
        g_is_running = true;

        if (g_shooter != null)
            g_shooter.GameStart();
    }

    public void GameEndBuffer() {
        g_is_running = false;

        if (g_shooter != null)
            g_shooter.GameEndingBuffer();
    }

    public void GameEnd() {
        if (g_shooter != null)
            g_shooter.GameEnd();
    }
#endregion

    public virtual void Fire() {

    }
}
