using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IDInput : MonoBehaviour {
    static string s_userId = "123";
    
    public static string UserID {
        get {
            return s_userId;
        }
    }

    public InputField g_inputField;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Submit() {
        s_userId = g_inputField.text;
        SceneManager.LoadScene("NewGameScene");
    }
}
