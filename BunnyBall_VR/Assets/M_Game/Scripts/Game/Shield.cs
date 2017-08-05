using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    float r_time = 0.0f;
    public OVRInput.Controller controller;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (r_time > 0.0f) {
            r_time -= Time.deltaTime;
            if (r_time <= 0.0f) {
                OVRInput.SetControllerVibration(0.0f, 0.0f, controller);
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if ((collision.gameObject.tag == "Bullet") || (collision.gameObject.tag == "BigBullet") || collision.gameObject.tag == "Soccer") {
            this.GetComponent<AudioSource>().Play();

            if (collision.gameObject.tag == "BigBullet") {
                OVRInput.SetControllerVibration(1.0f, 1.0f, controller);
                r_time = 1.0f;              
            }
            else {
                OVRInput.SetControllerVibration(0.5f, 0.5f, controller);
                r_time = 0.2f;
            }

            if (collision.transform.GetComponent<Bullet>().isBlocked == false) {
                collision.transform.GetComponent<Bullet>().isBlocked = true;
                SceneController.Level_Current.GetComponent<ScoreCalculation>().BulletBlocked();
            }
        }
    }
}
