using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTo : MonoBehaviour {
    bool isFlying = false;
    Vector3 m_start_position;
    public Transform m_end_transform;

    float m_journeyTime = 1.0f;
    float m_startTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isFlying) {
            float fracComplete = (Time.time - m_startTime) / m_journeyTime;
            if (fracComplete > 1.0f) {
                fracComplete = 1.0f;
                isFlying = false;
            }

            transform.position = Vector3.Slerp(m_start_position, m_end_transform.position, fracComplete);
        }

        if (Input.GetKeyUp(KeyCode.F)) {
            StartFly();
        }
	}

    void StartFly() {
        isFlying = true;
        m_startTime = Time.time;
        m_start_position = this.transform.position;
    }
}
