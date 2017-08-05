using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSoccer : Rabbit {
    protected override void Update() {
        if (!(m_gun as SoccerShootController).Is_powerHolding)
            base.Update();
    }
}
