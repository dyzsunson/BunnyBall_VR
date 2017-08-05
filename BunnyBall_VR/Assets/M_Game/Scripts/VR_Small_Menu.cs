using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VR_Small_Menu : MonoBehaviour {
    Transform m_showButton;
    Transform m_hideButton;
    Transform m_fullButton;
    Transform m_smallButton;

    Transform m_items;

    Image m_vr_icon;

    Image m_bg1;
    Image m_bg2;

    public Camera sub_vr_camera;

    public Camera UI_Camera;

    bool is_fullScreen = false;
    bool is_hide = false;

    private void Awake() {
        m_showButton = this.transform.Find("ShowButton");
        m_hideButton = this.transform.Find("HideButton");
        m_items = this.transform.Find("Items");
        m_fullButton = this.transform.Find("Items/FullButton");
        m_smallButton = this.transform.Find("Items/SmallButton");

        m_vr_icon = this.transform.Find("VRIcon").GetComponent<Image>();
        m_bg1 = this.transform.Find("BG1").GetComponent<Image>();
        m_bg2 = this.transform.Find("BG2").GetComponent<Image>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetImageAlpha(Image _image, float _a) {
        Color color = _image.color;
        color.a = _a;
        _image.color = color;
    }

    public void Hide() {
        is_hide = true;

        m_hideButton.gameObject.SetActive(false);
        m_showButton.gameObject.SetActive(true);

        sub_vr_camera.gameObject.SetActive(false);
        this.transform.localPosition = new Vector3(200.0f, this.transform.localPosition.y, 0.0f);

        this.SetImageAlpha(m_vr_icon, 0.9f);

        if (is_fullScreen) {
            UI_Camera.cullingMask |= (1 << LayerMask.NameToLayer("UI"));

            this.SetImageAlpha(m_bg1, 0.3f);
            this.SetImageAlpha(m_bg2, 0.3f);
        }
    }

    public void Show() {
        is_hide = false;

        m_hideButton.gameObject.SetActive(true);
        m_showButton.gameObject.SetActive(false);

        sub_vr_camera.gameObject.SetActive(true);

        this.transform.localPosition = new Vector3(0.0f, this.transform.localPosition.y, 0.0f);

        this.SetImageAlpha(m_vr_icon, 0.5f);

        if (is_fullScreen)
            Full();
    }

    public void Full() {
        if (is_hide)
            return;

        is_fullScreen = true;

        m_fullButton.gameObject.SetActive(false);
        m_smallButton.gameObject.SetActive(true);

        this.transform.localPosition = new Vector3(-585, 322, 0);
        sub_vr_camera.rect = new Rect(0.018f, 0.0f, 0.982f, 0.982f);

        this.SetImageAlpha(m_bg1, 1.0f);
        this.SetImageAlpha(m_bg2, 1.0f);

        m_items.localPosition = new Vector3(-120.0f, 0.0f, 0.0f);

        UI_Camera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI"));
    }

    public void ExitFull() {
        if (is_hide)
            return;

        is_fullScreen = false;

        m_fullButton.gameObject.SetActive(true);
        m_smallButton.gameObject.SetActive(false);

        this.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        sub_vr_camera.rect = new Rect(0.75f, 0.0f, 0.25f, 0.25f);

        this.SetImageAlpha(m_bg1, 0.3f);
        this.SetImageAlpha(m_bg2, 0.3f);

        m_items.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        UI_Camera.cullingMask |= (1 << LayerMask.NameToLayer("UI"));
    }

    public void ResetCamera() {
        UnityEngine.VR.InputTracking.Recenter();
    }
}
