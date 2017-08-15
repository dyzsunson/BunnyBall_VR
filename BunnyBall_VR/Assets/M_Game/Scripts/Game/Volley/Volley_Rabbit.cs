using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volley_Rabbit : RabbitCannon {
    protected override void Start() {
        base.Start();
        m_max_degree = 10.0f;
    }

}
