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

	// Use this for initialization
	void Start () {
        Physics.IgnoreLayerCollision(bulletPrefab.gameObject.layer, bulletPrefab.gameObject.layer);
    }

    // Update is called once per frame
    void Update () {
        if (r_time > 0.0f) {
            r_time -= Time.deltaTime;
            if (r_time <= 0.0f) {
                OVRInput.SetControllerVibration(0.0f, 0.0f, controller);
            }
        }


        float x = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);

        if (m_isHold == true && x < 0.5f) {
            TriggerUp();
        }
        else if (m_isHold == false && x > 0.7f) {
            TriggerDown();
        }

    }

    void TriggerDown() {
        m_isHold = true;
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = start_transform.position;
        bullet.transform.forward = (end_transform.position - start_transform.position).normalized;
        bullet.GetComponent<Rigidbody>().AddForce(300.0f * bullet.transform.forward);


        OVRInput.SetControllerVibration(0.8f, 0.8f, controller);
        this.GetComponent<AudioSource>().Play();

        r_time = 0.1f;
    }

    void TriggerUp() {
        m_isHold = false;
    }
}
