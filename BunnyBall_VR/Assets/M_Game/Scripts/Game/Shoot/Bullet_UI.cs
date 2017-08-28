using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_UI : MonoBehaviour {
    public GameObject p_bullet_prefab;
    public int p_num = 10;
    public float p_dis = 0.015f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < p_num; i++) {
            GameObject bullet = Instantiate(p_bullet_prefab);
            bullet.transform.SetParent(this.transform);
            bullet.transform.localPosition = new Vector3(p_dis * (i - p_num / 2.0f), 0.0f, 0.0f);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
