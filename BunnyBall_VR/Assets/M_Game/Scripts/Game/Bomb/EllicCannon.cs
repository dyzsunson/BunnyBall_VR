using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllicCannon : EllicControlller {
    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    public override void Fire() {
        m_animator.Play("Base Layer.CannonFire");
    }
}
