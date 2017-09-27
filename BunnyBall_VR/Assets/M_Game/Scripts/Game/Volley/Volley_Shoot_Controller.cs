using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volley_Shoot_Controller : ShootController  {
    float m_reload_gun_speed = 0.2f;
    public Volley_Basket[] basketArray;
    public Volley_Ball MoneyBall_Prefab;
    public ScoreBoard Double_Board;
    int m_basket_current;

    int m_total_num = 0;

    public Transform p_build_Soccer_Position;
    private GameObject m_current_volley;

    private float m_power_last;
    private GameObject m_last_volley;

    protected override void Start() {
        base.Start();
        m_max_degree = 0.0f;
        m_max_reloadTime = 2.0f;

        if (basketArray.Length > 0) {
            for (int i = 0; i < basketArray.Length; i++)
                basketArray[i].Basket_ID = i;
        }

        this.BuildVolley();
    }

    protected override GameObject Fire() {
        m_power = (m_power + 6.2f) / 7.5f;


        if (m_current_volley == null)
            return null;

        m_power_last = m_power;
        m_last_volley = m_current_volley;
        Invoke("LateFire", 0.35f);

        this.transform.parent.GetComponent<EllicControlller>().Fire();
        GameObject ball = m_current_volley;
        m_current_volley = null;

        m_last_volley.GetComponent<Rigidbody>().useGravity = true;
        m_last_volley.GetComponent<Rigidbody>().AddForce(150.0f * new Vector3(0.0f, 1.0f, 0.0f));

        return ball;
    }

    protected override void ReloadEnd() {
        base.ReloadEnd();
        this.BuildVolley();
    }

    void LateFire() {
        m_last_volley.GetComponent<Rigidbody>().useGravity = true;
        m_last_volley.GetComponent<Collider>().enabled = true;

        FireOneBullet(m_last_volley, m_power_last, new Vector3(0.0f, 0.0f, 0.0f));
        m_last_volley.GetComponent<Volley_Ball>().Fire();

        if (m_total_num % 5 == 0) {
            ScoreBoard board = Instantiate(Double_Board);
            board.transform.position = m_last_volley.transform.position + new Vector3(0.0f, 2.0f, 0.0f);
        }

        Invoke("HighLightBasket", 0.5f);
    }

    void HighLightBasket() {
        foreach (Volley_Basket basket in basketArray)
            basket.WrongBasket();
        basketArray[m_basket_current].RightBasket();
        Invoke("DeHighLightBasket", 2.5f);
    }

    void DeHighLightBasket() {
        foreach (Volley_Basket basket in basketArray)
            basket.WrongBasket();
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

    public override void GameStart() {
        base.GameStart();
        DeHighLightBasket();
    }

    void BuildVolley() {
        m_total_num++;
        if (m_total_num % 5 == 0) {
            m_current_volley = Instantiate(MoneyBall_Prefab).gameObject;
        }
        else
            m_current_volley = Instantiate(BulletPrefab);

        if (basketArray.Length > 0) {
            m_basket_current = Random.Range(0, basketArray.Length);
            m_current_volley.GetComponent<Volley_Ball>().Basket_ID = m_basket_current;
        }

        
        m_current_volley.transform.position = p_build_Soccer_Position.position;
        m_current_volley.transform.SetParent(this.transform.parent.parent);

        m_current_volley.GetComponent<Rigidbody>().useGravity = false;
        m_current_volley.GetComponent<Collider>().enabled = false;

        
    }

}
