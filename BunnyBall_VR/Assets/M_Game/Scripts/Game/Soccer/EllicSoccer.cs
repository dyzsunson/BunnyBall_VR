using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllicSoccer : EllicControlller {
    protected override void Start() {
        base.Start();
        m_max_degree = 18.0f;
    }
    protected override void Update() {
        if (!(g_shooter as SoccerShootController).Is_powerHolding)
            base.Update();
    }

    public override void Fire() {
        m_animator.Play("Base Layer.SoccerKick");
    }
}
