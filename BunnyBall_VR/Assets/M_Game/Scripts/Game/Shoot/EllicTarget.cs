using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllicTarget : MonoBehaviour {
    float m_life = 0;
    public float m_life_max = 3;
    public float m_escape_time = 5.0f;

    Transform life_sprite;

    float m_life_sprite_scaleX_ori;
    float m_life_sprite_positionX_ori;

	// Use this for initialization
	void Start () {
        life_sprite = this.transform.Find("Life");
        m_life = m_life_max;

        m_life_sprite_scaleX_ori = life_sprite.localScale.x;
        m_life_sprite_positionX_ori = life_sprite.localPosition.x;
        Invoke("Escape", m_escape_time);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (m_life > 0 && other.gameObject.tag == "Bullet") {
            m_life -= other.GetComponent<Skeet_Bullet>().p_attack;
            if (m_life <= 0)
                Die();
            ChangeLife();
            if (this.GetComponent<AudioSource>().isPlaying == false)
                this.GetComponent<AudioSource>().Play();
        }
    }

    private void Remove() {
        Destroy(this.gameObject);
    }

    private void Die() {
        Invoke("Remove", 1.0f);
    }

    private void Escape() {
        Invoke("Remove", 1.0f);
    }

    private void ChangeLife() {
        life_sprite.localScale = new Vector3(m_life / m_life_max * m_life_sprite_scaleX_ori, life_sprite.localScale.y, life_sprite.localScale.z);
    }
}
