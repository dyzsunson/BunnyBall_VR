using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Pages : MonoBehaviour {
    // Use this for initialization
    int m_current_page = 0;
    int m_total_page;

    public Text page_num_text;

    float m_fade_time = 0.5f;

	void Start () {
        m_total_page = this.transform.childCount;
        this.ChangePage(0, m_current_page);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LastPage() {
        if (m_current_page > 0) {
            m_current_page--;
            this.ChangePage(m_current_page + 1, m_current_page);
        }
    }

    public void NextPage() {
        if (m_current_page < m_total_page - 1) {
            m_current_page++;
            this.ChangePage(m_current_page - 1, m_current_page);
        }
    }

    void ChangePage(int _last, int _current) {        
        this.transform.GetChild(_last).gameObject.SetActive(false);
        this.transform.GetChild(_current).gameObject.SetActive(true);

        this.ChangeText();
    }

    void ChangeText() {
        page_num_text.text = "" + (m_current_page + 1) + "/" + m_total_page;
    }
}
