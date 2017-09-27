using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerShootController : ShootController {
    float m_curve_power = 0.0f;
    float m_max_curve_power = 1.2f;

    public GameObject Curve_UI;
    public Power_Curve power_curve;

    Transform m_curve_ui_cursor;

    public Transform Build_Soccer_Position;
    private GameObject m_current_soccer;

    private float m_curve_power_last;
    private float m_power_last;
    private GameObject m_last_soccer;

    private Transform m_gun_body;
    public bool Is_powerHolding {
        get {
            return Able_Fire && InputCtrl.IsPowerButton;
        }
    }

    // Use this for initialization
    protected override void Start() {
        base.Start();
        m_scaler *= 1.1f;
        m_max_reloadTime *= 3.0f;
        m_allow_power_reloading = false;

        BuildSoccer();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();

        if (Able_Fire && !is_reloading) {
            if (Is_powerHolding) {
                if (InputCtrl.IsLeftButton)
                    m_curve_power -= 3.0f * Time.deltaTime;
                if (InputCtrl.IsRightButton)
                    m_curve_power += 3.0f * Time.deltaTime;

                if (m_curve_power > m_max_curve_power)
                    m_curve_power = m_max_curve_power;
                if (m_curve_power < -m_max_curve_power)
                    m_curve_power = -m_max_curve_power;
            }
            m_curve_ui_cursor.localPosition = new Vector3(m_curve_power * 20.0f, 0.0f, 0.0f);           
        }
        power_curve.SetPowerAndCurve(m_power - m_min_power, m_max_power - m_min_power, m_curve_power, m_max_curve_power);
    }

    protected override void Reloading() {
        base.Reloading();
    }

    protected override void ReloadEnd() {
        base.ReloadEnd();
        m_curve_power = 0.0f;
        BuildSoccer();
    }

    protected override void ResetFire() {
        base.ResetFire();
    }

    protected override GameObject Fire() {
        if (m_current_soccer == null)
            return null;

        m_power_last = m_power;
        m_curve_power_last = m_curve_power;
        m_last_soccer = m_current_soccer;
        Invoke("LateFire", 0.6f);

        this.transform.parent.GetComponent<EllicControlller>().Fire();

        GameObject soccer = m_current_soccer;
        m_current_soccer = null;
        return soccer;
    }

    void LateFire() {
        m_last_soccer.GetComponent<SoccerBall>().Fire(new Vector3(1.25f * m_curve_power_last, 0.0f, 0.0f));
        FireOneBullet(m_last_soccer, m_power_last, new Vector3(m_curve_power_last * -0.5f, 0.0f, -0.3f));
    }

    public override void GameReady() {
        base.GameReady();
        Curve_UI.SetActive(true);
        m_curve_ui_cursor = Curve_UI.transform.Find("curve_cursor");
    }

    void BuildSoccer() {
        m_current_soccer = Instantiate(BulletPrefab);
        m_current_soccer.transform.position = Build_Soccer_Position.position;
        m_current_soccer.transform.SetParent(this.transform.parent.parent);
    }
}
