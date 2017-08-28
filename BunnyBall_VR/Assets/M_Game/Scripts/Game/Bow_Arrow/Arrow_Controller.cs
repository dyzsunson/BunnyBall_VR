using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Controller : Skeet_Bullet {
    Rigidbody m_rigidbody;
    public Transform p_center_transform;
    public Rigidbody p_tail;

    private void Awake() {
        m_rigidbody = this.GetComponent<Rigidbody>();
        m_rigidbody.centerOfMass = 30.0f * p_center_transform.localPosition;
    }

    protected override void Start() {
        // base.Start();
    }

    protected override void Update() {
        // base.Update();
    }


    protected override void HitObjects(GameObject _other) {
        // base.HitObjects(_other);
        m_rigidbody.useGravity = false;
        p_tail.useGravity = false;
        m_rigidbody.velocity = p_tail.velocity = Vector3.zero;
        this.GetComponent<Collider>().enabled = false;
        p_tail.GetComponent<Collider>().enabled = false;

        // this.transform.SetParent(_other.transform, true);

        Destroy(this.GetComponent<Joint>());
        Destroy(m_rigidbody);
        Destroy(p_tail);

        Invoke("End", 10.0f);
    }

    public void Fire(float _power) {
        this.GetComponent<Collider>().enabled = true;
        p_tail.GetComponent<Collider>().enabled = true;
        m_rigidbody.AddForce(_power * this.transform.forward);
        m_rigidbody.useGravity = true;
        p_tail.useGravity = true;
        // Invoke("End", )
    }
}
