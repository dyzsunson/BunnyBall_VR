using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_GunController : MonoBehaviour {
    public Transform start_transform;
    public Transform end_transform;

    public OVRInput.Controller controller;

    bool m_isHold = false;

    public Bullet bulletPrefab;
    float r_time = 0.0f;

    int m_bullet_num = 10;
    int m_bullet_num_max = 10;

    bool m_is_reloading = false;
    private List<float> m_distance_array = new List<float>();
    private int m_check_length = 15;
    private float m_ok_distance = 30.0f;

    public AudioSource p_reload_audio;
    public AudioSource p_fire_audio;

    public Bullet_UI p_bullet_ui;

    // Use this for initialization
    void Start () {
        Physics.IgnoreLayerCollision(bulletPrefab.gameObject.layer, bulletPrefab.gameObject.layer);
        m_bullet_num = m_bullet_num_max;

        if (p_bullet_ui != null) {
            p_bullet_ui.SetCurrentBullet(m_bullet_num);
        }
    }

    // Update is called once per frame
    void Update () {
        if (r_time > 0.0f) {
            r_time -= Time.deltaTime;
            if (r_time <= 0.0f) {
                OVRInput.SetControllerVibration(0.0f, 0.0f, controller);
            }
        }

        if (m_is_reloading == false) {
            float x = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);

            if (m_isHold == true && x < 0.5f) {
                TriggerUp();
            }
            else if (m_isHold == false && x > 0.7f) {
                TriggerDown();
            }
        }
    }

    private void FixedUpdate() {
        float degree = this.transform.eulerAngles.x;
        if (degree > 180.0f)
            degree -= 360.0f;

        m_distance_array.Add(degree);
        if (m_distance_array.Count > m_check_length) {
            m_distance_array.RemoveAt(0);
        }

        float max_changed = 0.0f;
        float min_num = 1000.0f;
        float max_num = -1000.0f;

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
            m_bullet_num = m_bullet_num_max;
            m_is_reloading = false;

            if (p_bullet_ui != null) {
                p_bullet_ui.SetCurrentBullet(m_bullet_num);
            }

            this.p_reload_audio.Play();
            // p_reload_audio.Play();
        }
    }

    void TriggerDown() {
        m_isHold = true;
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = start_transform.position;
        bullet.transform.forward = (end_transform.position - start_transform.position).normalized;
        bullet.GetComponent<Rigidbody>().AddForce(300.0f * bullet.transform.forward);


        OVRInput.SetControllerVibration(0.8f, 0.8f, controller);
        this.p_fire_audio.Play();

        r_time = 0.1f;

        m_bullet_num--;
        if (m_bullet_num <= 0) {
            m_is_reloading = true;
        }

        if (p_bullet_ui != null) {
            p_bullet_ui.SetCurrentBullet(m_bullet_num);
        }

    }

    void TriggerUp() {
        m_isHold = false;
    }
}
