using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volley_Ball : Bullet {
    private bool is_in_basket = false;
    public int Basket_ID = 0;

    public PhysicMaterial Basket_Material;

    public bool Is_in_basket {
        get {
            return this.is_in_basket;
        }
    }

    protected override void Start() {
        base.Start();
        m_lifeTime = 15.0f;
    }

    protected override void End() {
        if (is_in_basket == false)
            base.End();
    }

    public void EnterBasket() {
        is_in_basket = true;
        this.GetComponent<SphereCollider>().material = Basket_Material;
    }
}
