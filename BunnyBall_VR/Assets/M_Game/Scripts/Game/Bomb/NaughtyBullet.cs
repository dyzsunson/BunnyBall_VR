using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaughtyBullet : Bullet {
    private void FixedUpdate() {
        this.GetComponent<Rigidbody>().AddForce(100.0f * new Vector3(Random.value - 0.5f, 0.0f, 0.0f));
    }
}
