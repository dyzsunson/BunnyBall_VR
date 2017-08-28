using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow_Controller : MonoBehaviour {
    public Arrow_Controller p_arrow_prefab;
    public Transform p_lefthand_transform;
    public Transform p_righthand_transform;

    enum Bow_State {
        Waiting = 0,
        Holding = 1,
        Pulling = 2
    }

    Bow_State m_bow_state = Bow_State.Waiting;
    Arrow_Controller m_arrow = null;

    float m_load_distance = 1.25f;
    bool m_isHold = false;

    Transform m_up_transform;
    Transform m_down_transform;
    LineRenderer m_bowstring;

    private void Awake() {
        m_up_transform = this.transform.Find("Bow/Up");
        m_down_transform = this.transform.Find("Bow/Down");
        m_bowstring = this.GetComponent<LineRenderer>();
    }

    // Use this for initialization
    void Start () {
        Invoke("Create_Arrow", 1.0f);
        this.Update_Bowstring();
    }
	
	// Update is called once per frame
	void Update () {
        float x = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);

        if (m_isHold == true && x < 0.5f) {
            TriggerUp();
        }
        else if (m_isHold == false && x > 0.7f) {
            TriggerDown();
        }
    }

    private void LateUpdate() {
        if (m_bow_state == Bow_State.Holding) {
            m_arrow.transform.position = p_righthand_transform.position;
            m_arrow.transform.rotation = p_righthand_transform.rotation;
        }
        else if (m_bow_state == Bow_State.Pulling) {
            m_arrow.transform.position = p_righthand_transform.position;
            m_arrow.transform.forward = p_lefthand_transform.position - p_righthand_transform.position;
        }

        this.Update_Bowstring();
    }

    void Create_Arrow() {
        m_arrow = Instantiate(p_arrow_prefab);
        m_arrow.transform.SetParent(this.transform.parent.parent);
        m_bow_state = Bow_State.Holding;
    }

    void Update_Bowstring() {
        m_bowstring.SetPosition(0, m_up_transform.position);
        m_bowstring.SetPosition(2, m_down_transform.position);

        if (m_bow_state == Bow_State.Waiting || m_bow_state == Bow_State.Holding) {
            m_bowstring.SetPosition(1, 0.5f * (m_up_transform.position + m_down_transform.position));
        }
        else if (m_bow_state == Bow_State.Pulling) {
            m_bowstring.SetPosition(1, p_righthand_transform.position);
        }

        
    }

    void Fire() {
        m_arrow.GetComponent<LineRenderer>().enabled = false;
        this.GetComponent<AudioSource>().Play();


        float power = Vector3.Distance(m_up_transform.position, p_righthand_transform.position)
            + Vector3.Distance(m_down_transform.position, p_righthand_transform.position)
            - Vector3.Distance(m_up_transform.position, m_down_transform.position);

        m_arrow.Fire(power * 3500.0f);
        m_arrow = null;
        m_bow_state = Bow_State.Waiting;
        Invoke("Create_Arrow", 0.25f);
    }

    void TriggerUp() {
        m_isHold = false;

        if (m_bow_state == Bow_State.Pulling) {
            Fire();
        }
    }

    void TriggerDown() {
        m_isHold = true;

        if (m_bow_state == Bow_State.Holding && Vector3.Distance(p_lefthand_transform.position, p_righthand_transform.position) < m_load_distance) {
            m_bow_state = Bow_State.Pulling;
            m_arrow.GetComponent<LineRenderer>().enabled = true;
        }
    }
}
