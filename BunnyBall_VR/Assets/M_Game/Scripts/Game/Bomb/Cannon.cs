using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : ShootController {
    public GameObject BigBulletPrefab;
    public GameObject NaughtyBulletPrefab;
    public GameObject ThreeBulletPrefab;

    float m_reload_gun_speed = 1.0f;
    Vector3 m_gun_prePosition;

    enum SkillID {
        BigBullet = 0, 
        ThreeBullet = 1,
        NaughtyBullet = 2
    }

    float m_firePower = 0.0f;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        m_gun_prePosition = GunBodyTransform.localPosition;
	}

    // Update is called once per frame
    protected override void Update () {
        base.Update();
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

    protected override void ReloadEnd() {
        base.ReloadEnd();
        GunBodyTransform.localPosition = m_gun_prePosition;
    }

    void FireContinuousBullet() {
        GameObject bullet = Instantiate(ThreeBulletPrefab) as GameObject;
        this.FireOneBullet(bullet, m_firePower);
    }

    protected override GameObject Fire() {
        m_firePower = m_power;

        GameObject bullet = null;
        if (skill_array.Length > (int)SkillID.ThreeBullet
                && skill_array[(int)SkillID.ThreeBullet].is_working) {
            
            bullet = Instantiate(ThreeBulletPrefab) as GameObject;
            Invoke("FireContinuousBullet", 0.2f);
            Invoke("FireContinuousBullet", 0.4f);
        }
        else if (skill_array.Length > (int)SkillID.BigBullet && skill_array[(int)SkillID.BigBullet].is_working)
            bullet = Instantiate(BigBulletPrefab) as GameObject;
        else if (skill_array.Length > (int)SkillID.NaughtyBullet && skill_array[(int)SkillID.NaughtyBullet].is_working)
            bullet = Instantiate(NaughtyBulletPrefab) as GameObject;
        else
            bullet = Instantiate(BulletPrefab) as GameObject;

        if (skill_array.Length > (int)SkillID.BigBullet && skill_array[(int)SkillID.BigBullet].is_working)
            FireOneBullet(bullet, 100.0f * m_power);
        else
            FireOneBullet(bullet, m_power);

        this.transform.parent.GetComponent<EllicControlller>().Fire();

        return bullet;
    }
}
