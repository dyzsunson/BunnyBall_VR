﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public GameObject BubblePrefab;

    float m_lifeTime = 5.0f;
    float m_bottom = 0.0f;

    bool is_inWater = false;

    public bool isBlocked = false;

	// Use this for initialization
	protected virtual void Start () {
        Invoke("End", m_lifeTime);
        if (this.tag == "BigBullet")
            m_bottom = 0.1f;
        else
            m_bottom = -1.1f;
	}
	
	// Update is called once per frame
	void Update () {
        if (!is_inWater && this.transform.position.y < m_bottom) {
            GameObject bubble = Instantiate(BubblePrefab) as GameObject;
            bubble.transform.position = this.transform.position;
            is_inWater = true;

            this.transform.GetComponent<Rigidbody>().velocity *= 0.5f;
            this.transform.GetComponent<SphereCollider>().enabled = false;
            // End();
        }
	}

    void End() {
        Destroy(this.gameObject);
    }
}
