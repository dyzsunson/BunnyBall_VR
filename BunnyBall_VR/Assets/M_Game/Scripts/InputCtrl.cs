using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCtrl : MonoBehaviour {
    public static InputCtrl context;

    public bool Is_AI_Ctrl = false;

    public static bool IsLeftButton {
        get {
            if (context.Is_AI_Ctrl)
                return SceneController.AI_Current.IsLeftHold;
            else
                return Input.GetKey(KeyCode.LeftArrow); // || Input.GetAxis("Horizontal") < -0.1f; 
        }
    }

    public static bool IsRightButton {
        get {
            if (context.Is_AI_Ctrl)
                return SceneController.AI_Current.IsRightHold;
            else
                return Input.GetKey(KeyCode.RightArrow); // || Input.GetAxis("Horizontal") > 0.1f;
        }
    }

    public static bool IsUpButton {
        get {
            if (context.Is_AI_Ctrl)
                return SceneController.AI_Current.IsUpHold;
            else
                return Input.GetKey(KeyCode.UpArrow); // || Input.GetAxis("Vertical") < -0.1f;
        }
    }

    public static bool IsDownButton {
        get {
            if (context.Is_AI_Ctrl)
                return SceneController.AI_Current.IsDownHold;
            else
                return Input.GetKey(KeyCode.DownArrow); // || Input.GetAxis("Vertical") > 0.1f;
        }
    }

    public static bool IsPowerButton {
        get {
            if (context.Is_AI_Ctrl)
                return SceneController.AI_Current.IsPowerButtonHold;
            else
                return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0);
        }
    }

    public static bool IsPowerButtonUp {
        get {
            if (context.Is_AI_Ctrl)
                return SceneController.AI_Current.IsPowerButtonUp;
            else
                return Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.JoystickButton0);
        }
    }

    private void Awake() {
        context = this;
    }

    public static bool IsHotKeyDown(KeyCode _key) {
        if (context.Is_AI_Ctrl)
            return SceneController.AI_Current.IsHotKeyDown(_key);
        else
            return Input.GetKeyDown(_key);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
