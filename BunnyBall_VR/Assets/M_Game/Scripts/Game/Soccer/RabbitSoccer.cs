using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSoccer : Rabbit {
    protected override void Start() {
        base.Start();
        m_max_degree = 18.0f;
    }
    protected override void Update() {
        if (!(m_gun as SoccerShootController).Is_powerHolding)
            base.Update();
    }

    public override void Fire() {
        animator.Play("Base Layer.SoccerKick");
    }
}
