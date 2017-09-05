using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_UI : MonoBehaviour {
    public GameObject p_bullet_prefab;
    public GameObject p_bullet_empty_prefab;

    public int p_num = 10;
    public float p_dis = 0.015f;

    private GameObject[] m_bullet_array;
    private GameObject[] m_bullet_empty_array;

    public GameObject p_red_background;
    public GameObject p_green_background;

    private void Awake() {
        m_bullet_array = new GameObject[p_num];
        m_bullet_empty_array = new GameObject[p_num];

        for (int i = 0; i < p_num; i++) {
            GameObject bullet = Instantiate(p_bullet_prefab);
            GameObject bullet_empty = Instantiate(p_bullet_empty_prefab);

            bullet.transform.SetParent(this.transform);
            bullet_empty.transform.SetParent(this.transform);

            bullet.transform.localPosition = bullet_empty.transform.localPosition = new Vector3(p_dis * (i - p_num / 2.0f), 0.0f, 0.0f);
            
            m_bullet_array[i] = bullet;
            m_bullet_empty_array[i] = bullet_empty;
        }
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetCurrentBullet(int _num) {
        for (int i = 0; i < _num; i++) {
            m_bullet_array[i].SetActive(true);
            m_bullet_empty_array[i].SetActive(false);
        }
        for (int i = _num; i < p_num; i++) {
            m_bullet_array[i].SetActive(false);
            m_bullet_empty_array[i].SetActive(true);
        }

        if (_num == 0) {
            p_red_background.SetActive(true);
            p_green_background.SetActive(false);
        }
        else {
            p_red_background.SetActive(false);
            p_green_background.SetActive(true);
        }
    }
}
