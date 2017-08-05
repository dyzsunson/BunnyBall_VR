using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {
    bool isBreak = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Bullet" || 
            other.gameObject.tag == "BigBullet") {
            this.GetComponent<AudioSource>().Play();
            // Destroy(other.gameObject);
            this.GetComponent<MeshRenderer>().enabled = false;
            foreach (MeshRenderer render in this.transform.GetComponentsInChildren<MeshRenderer>())
                render.enabled = false;

            this.GetComponent<BoxCollider>().enabled = false;

            if (!isBreak) {
                SceneController.Level_Current.GetComponent<Bomb_ScoreCal>().EggBreak();
                isBreak = true;
            }
            // Destroy(this.gameObject);
        }
    }

    /*private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Bullet") {
            this.GetComponent<AudioSource>().Play();
            // Destroy(collision.gameObject);
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;

            if (!isBreak) {
                SceneController.EggBreak();
                isBreak = true;
            }
            // Destroy(this.gameObject);
        }
    }*/
}
