using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle_Gun : MonoBehaviour {
    public Transform left_hand;
    public Transform right_hand;

    bool m_isHold = false;

    float r_time = 0.0f;

    public Bullet p_bullet_prefab;
    private float m_fire_power = 800.0f;

    public Transform p_start_transform;
    public Transform p_end_transform;

    public AudioSource p_reload_audio;
    public AudioSource p_fire_audio;

    // private bool m_is_reloading = false;
    private List<float> m_distance_array = new List<float>();
    private int m_check_length = 30;
    private float m_ok_distance = 0.5f;

    private int m_bullet_num;
    private int m_bullet_max_num = 5;

    public Bullet_UI p_bullet_ui;

    // Use this for initialization
    void Start () {
        m_bullet_num = m_bullet_max_num;

        if (p_bullet_ui != null) {
            p_bullet_ui.SetCurrentBullet(m_bullet_num);
        }
    }

    private void Update() {
        if (r_time > 0.0f) {
            r_time -= Time.deltaTime;
            if (r_time <= 0.0f) {
                OVRInput.SetControllerVibration(0.0f, 0.0f, OVRInput.Controller.LTouch);
                OVRInput.SetControllerVibration(0.0f, 0.0f, OVRInput.Controller.RTouch);
            }
        }


        if (m_bullet_num > 0) { // m_is_reloading == false
            float x = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);

            if (m_isHold == true && x < 0.5f) {
                TriggerUp();
            }
            else if (m_isHold == false && x > 0.7f) {
                TriggerDown();
            }
        }
    }

    private void FixedUpdate() {
        m_distance_array.Add(Vector3.Distance(left_hand.position, right_hand.position));
        if (m_distance_array.Count > m_check_length) {
            m_distance_array.RemoveAt(0);
        }

        float max_changed = 0.0f;
        float min_num = 100.0f;
        float max_num = 0.0f;

        foreach (float num in m_distance_array) {               
            if (num > max_num) {
                max_num = num;
                min_num = num;
            }
            else if (num < min_num) {
                min_num = num;
                if (max_num - min_num > max_changed) {
                    max_changed = max_num - min_num;
                }
            }
        }

        if (max_changed > m_ok_distance) {
            m_distance_array.Clear();
            if (m_bullet_num < m_bullet_max_num) {
                m_bullet_num++;
                if (p_bullet_ui != null) {
                    p_bullet_ui.SetCurrentBullet(m_bullet_num);
                }
            }

            p_reload_audio.Play();
        }
    }

    void TriggerDown() {
        m_isHold = true;
        Bullet bullet = Instantiate(p_bullet_prefab);
        bullet.transform.position = p_start_transform.position;
        bullet.transform.forward = (p_end_transform.position - p_start_transform.position).normalized;
        bullet.GetComponent<Rigidbody>().AddForce(m_fire_power * bullet.transform.forward);


        OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0.8f, 0.8f, OVRInput.Controller.RTouch);
        p_fire_audio.Play();

        m_bullet_num--;

        if (p_bullet_ui != null) {
            p_bullet_ui.SetCurrentBullet(m_bullet_num);
        }
        // m_is_reloading = true;

        r_time = 0.1f;
    }

    void TriggerUp() {
        m_isHold = false;
    }

    // Update is called once per frame
    void LateUpdate () {
        this.transform.position = right_hand.position;
        Vector3 t_forward = left_hand.position - right_hand.position;
        if (t_forward == Vector3.zero)
            t_forward = new Vector3(0.0f, 0.0f, 1.0f);
        this.transform.forward = t_forward;
    }
}
