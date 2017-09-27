using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllicVolley: EllicCannon {
    protected override void Start() {
        base.Start();
        m_max_degree = 10.0f;
    }

    public override void Fire() {
        m_animator.Play("Base Layer.VolleyHit");
    }

}
