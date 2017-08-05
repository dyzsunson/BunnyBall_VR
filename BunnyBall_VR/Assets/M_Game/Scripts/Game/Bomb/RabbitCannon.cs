using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitCannon : Rabbit {
    private Animator animator;

    protected override void Start() {
        base.Start();
        animator = GetComponent<Animator>();
    }

    public override void Fire() {
        animator.Play("Base Layer.Hit");
    }
}
