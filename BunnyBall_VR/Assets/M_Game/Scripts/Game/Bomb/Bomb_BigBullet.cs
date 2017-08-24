using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_BigBullet : Bullet {

    protected override void Start() {
        base.Start();
        m_bottom = 0.1f;
    }
}
