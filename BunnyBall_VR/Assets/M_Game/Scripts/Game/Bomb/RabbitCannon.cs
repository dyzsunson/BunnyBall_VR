using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitCannon : Rabbit {
    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    public override void Fire() {
        animator.Play("Base Layer.CannonFire");
    }
}
