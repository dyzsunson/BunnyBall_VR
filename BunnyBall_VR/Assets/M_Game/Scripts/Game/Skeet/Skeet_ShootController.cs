using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeet_ShootController : ShootController {
    float m_reload_gun_speed = 1.0f;
    public Volley_Ball MoneyBall_Prefab;

    int m_total_num = 0;

    protected override void Start() {
        base.Start();
        m_max_degree = 0.0f;
        m_max_reloadTime = 0.35f;
    }

    protected override GameObject Fire() {
        m_power = (m_power + 6.0f) / 3.0f;

        GameObject ball = null;
        m_total_num++;
        /*if (m_total_num % 5 == 0) {
            ball = base.Fire(MoneyBall_Prefab.gameObject);
        }
        else*/
            ball = base.Fire();


        this.transform.parent.GetComponent<EllicControlller>().Fire();
        return ball;
    }

    protected override void Reloading() {
        base.Reloading();
        if (m_reloadTime < m_max_reloadTime / 2.0f) {
            GunBodyTransform.Translate(-m_reload_gun_speed * Time.deltaTime * new Vector3(0.0f, 0.0f, 1.0f));
        }
        else {
            GunBodyTransform.Translate(m_reload_gun_speed * Time.deltaTime * new Vector3(0.0f, 0.0f, 1.0f));
        }
    }
}
