using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volley_Shoot_Controller : ShootController  {
    float m_reload_gun_speed = 0.2f;
    public Volley_Basket[] basketArray;
    int m_basket_current;

    protected override void Start() {
        base.Start();
        m_max_degree = 0.0f;
        m_max_reloadTime = 2.0f;
    }

    protected override GameObject Fire() {
        m_power = (m_power + 6.0f) / 7.5f;
        GameObject ball = base.Fire();

        m_basket_current = Random.Range(0, basketArray.Length);
        ball.GetComponent<Volley_Ball>().Basket_ID = m_basket_current;
        Invoke("HighLightBasket", 1.0f);

        this.transform.parent.GetComponent<Rabbit>().Fire();
        return ball;
    }

    void HighLightBasket() {
        foreach (Volley_Basket basket in basketArray)
            basket.WrongBasket();
        basketArray[m_basket_current].RightBasket();
        Invoke("DeHighLightBasket", 2.0f);
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
}
