using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeet_Bullet : Bullet {
    public GameObject SmokePrefab;
    public int p_attack = 1;

    private void OnCollisionEnter(Collision collision) {
        HitObjects(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        HitObjects(other.gameObject);
    }

    protected virtual void HitObjects(GameObject _other) {
        Destroy(this.gameObject);
        GameObject smoke = Instantiate(SmokePrefab);
        smoke.transform.position = this.transform.position;
    }
}
