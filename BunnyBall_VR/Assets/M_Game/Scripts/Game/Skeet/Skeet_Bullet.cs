using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeet_Bullet : Bullet {
    public GameObject SmokePrefab;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag != "Bullet") {
            Destroy(this.gameObject);
            GameObject smoke = Instantiate(SmokePrefab);
            smoke.transform.position = this.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(this.gameObject);
        GameObject smoke = Instantiate(SmokePrefab);
        smoke.transform.position = this.transform.position;
    }
}
