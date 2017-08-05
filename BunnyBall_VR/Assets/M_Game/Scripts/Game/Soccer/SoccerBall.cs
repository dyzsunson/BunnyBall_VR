using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBall : Bullet {
    public bool is_in = false;

    private bool is_Hit = false;
    private bool is_rotating = true;

    float scale = 0.15f;

    public Vector3 M_Force;

    protected override void Start() {
        
    }

    public void Fire(Vector3 _power) {
        base.Start();
        Invoke("StartRotate", 0.3f);
        M_Force = _power;
    }

    private void StartRotate() {
        is_rotating = true;
        scale = 0.3f;
        is_Hit = false;
    }

    private void FixedUpdate() {
        if (!is_Hit && is_rotating) {
            float v = this.GetComponent<Rigidbody>().velocity.magnitude * scale;
            float r = this.GetComponent<Rigidbody>().angularVelocity.magnitude * 1.0f;

            this.GetComponent<Rigidbody>().AddForce(v * M_Force);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        is_Hit = true;
    }
}
