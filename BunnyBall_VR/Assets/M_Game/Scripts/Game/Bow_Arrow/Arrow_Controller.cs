using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Controller : Skeet_Bullet {
    Rigidbody m_rigidbody;
    public Transform g_center_transform;
    public Rigidbody g_tail;

    bool m_is_working = false;

    private void Awake() {
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_rigidbody.centerOfMass = 30.0f * g_center_transform.localPosition;
    }

    protected override void Start() {
        // base.Start();
    }

    protected override void Update() {
        if (m_is_working == true && !is_inWater && this.g_center_transform.position.y < m_bottom) {
            GameObject bubble = Instantiate(BubblePrefab) as GameObject;
            bubble.transform.position = this.transform.position;
            is_inWater = true;
            this.HitObjects(null);
        }
    }


    protected override void HitObjects(GameObject _other) {
        // base.HitObjects(_other);
        if (!m_is_working)
            return;
        m_is_working = true;

        if (!is_inWater && this.g_center_transform.position.y < m_bottom) {
            GameObject bubble = Instantiate(BubblePrefab) as GameObject;
            bubble.transform.position = this.transform.position;
            is_inWater = true;
            _other = null;
        }

        this.GetComponent<Collider>().enabled = false;
        g_tail.GetComponent<Collider>().enabled = false;

        if (_other != null) {
            m_rigidbody.useGravity = false;
            g_tail.useGravity = false;
            m_rigidbody.velocity = g_tail.velocity = Vector3.zero;
           
            if (_other.tag == "Target") {
                this.transform.SetParent(_other.transform.parent, true);
                _other.transform.parent.GetComponent<Target_Controller>().OnArrowEnter(this);
            }
            Destroy(this.GetComponent<Joint>());
            Destroy(m_rigidbody);
            Destroy(g_tail);
        }
        else {
            m_rigidbody.velocity *= 0.5f;
            g_tail.velocity *= 0.5f;
        }

        SceneController.Level_Current.GetComponent<Arrow_Score_Cal>().BulletFired();

        Invoke("End", 10.0f);
    }

    public void Fire(float _power) {
        m_is_working = true;
        this.GetComponent<Collider>().enabled = true;
        g_tail.GetComponent<Collider>().enabled = true;
        m_rigidbody.AddForce(_power * this.transform.forward);
        m_rigidbody.useGravity = true;
        g_tail.useGravity = true;
        // Invoke("End", )
    }
}
