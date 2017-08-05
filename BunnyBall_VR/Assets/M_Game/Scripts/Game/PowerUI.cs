using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUI : MonoBehaviour {
    Color gray = new Color(0.1f, 0.1f, 0.1f, 0.25f);
    Color green = new Color(1f, 1f, 0.1f, 0.25f);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPower(int power) {
        power /= 10;
        for (int i = 1; i <= power; i++) {
            Color m_green = new Color(i / 12.0f, (10 - i) / 12.0f, 0.1f, 0.8f);
            this.transform.Find("Image" + i).GetComponent<Image>().color = m_green;
        }
        for (int i = power + 1; i <= 10; i++) {
            this.transform.Find("Image" + i).GetComponent<Image>().color = gray;
        }
    }
}
