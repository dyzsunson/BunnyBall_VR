using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Controller : MonoBehaviour, Game_Process_Interface {
    enum Target_State {
        WAITING = 0,
        FLASHING = 1,
        STATE1 = 2,
        STATE2 = 3,
        STATE3 = 4,
        STATE4 = 5,
        END = 6
    }

    Target_State m_state = Target_State.WAITING;

    Transform m_target;

    float m_z_speed = -1.0f;
    float m_x_speed = 1.0f;
    float m_inverse_time = 5.0f;

    float m_state_time = 0.0f;
    float m_inverse_current_time = 0.0f;

    private void Awake() {
        m_target = this.transform.Find("Target");
    }

    public ScoreBoard Board_Prefab;
    public Transform g_z_arrow;
    public Transform g_x_arrow;

    float[] m_score_dis_array = { 0.4f, 0.7f, 1.0f, 1.2f, 1.45f };

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {		
        if (m_state >= Target_State.STATE1 && m_state <= Target_State.STATE4) {
            if (m_state == Target_State.STATE2) {
                this.transform.Translate(Time.deltaTime * new Vector3(0.0f, 0.0f, m_z_speed));
            }

            if (m_state == Target_State.STATE3) {
                this.transform.Translate(Time.deltaTime * new Vector3(m_x_speed, 0.0f, 0.0f));
            }

            if (m_state == Target_State.STATE4) {
                this.transform.Translate(Time.deltaTime * new Vector3(m_x_speed, 0.0f, 0.0f));
            }


            m_state_time += Time.deltaTime;
            if (m_state_time >= 15.0f) {
                StartNextState();
            }

            if (m_state >= Target_State.STATE2 && m_state <= Target_State.STATE4) {
                m_inverse_current_time += Time.deltaTime;
                if (m_inverse_current_time >= m_inverse_time) {
                    m_inverse_current_time -= m_inverse_time;
                    InverseSpeed();
                }
            }
        }
	}

    void StartNextState() {
        m_state++;
        m_state_time = 0.0f;
        m_inverse_current_time = 0.0f;

        if (m_state == Target_State.STATE4) {
            m_inverse_time /= 2.0f;
            m_x_speed *= 2.0f;
        }

        if (m_state == Target_State.STATE4 || m_state == Target_State.STATE3) {
            m_inverse_current_time = m_inverse_time / 2.0f;
        }
        else
            m_inverse_current_time = 0.0f;

        if (m_state == Target_State.STATE2) {
            g_z_arrow.gameObject.SetActive(true);
        }
        else if (m_state == Target_State.STATE3) {
            g_z_arrow.gameObject.SetActive(false);
            g_x_arrow.gameObject.SetActive(true);
        }
       
    }

    void InverseSpeed() {
        m_z_speed = -m_z_speed;
        m_x_speed = -m_x_speed;

        g_z_arrow.transform.Rotate(180.0f * new Vector3(0.0f, 1.0f, 0.0f));
        g_x_arrow.transform.Rotate(180.0f * new Vector3(0.0f, 1.0f, 0.0f));
        // Invoke("InverseSpeed", m_inverse_time);
    }

    public void OnArrowEnter(Arrow_Controller _arrow) {
        float dis = Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(_arrow.g_center_transform.position.x, _arrow.g_center_transform.position.y));
        int score = 5;
        for (int i = 0; i < 4; i++) {
            if (dis < m_score_dis_array[i]) {
                break;
            }
            else
                score--;
        }

        ScoreBoard score_board = Instantiate(Board_Prefab);
        score_board.transform.position = _arrow.g_center_transform.position + new Vector3(0.0f, 0.5f, 1.0f);

        SceneController.Level_Current.GetComponent<Arrow_Score_Cal>().Arrow_Target(score);

        score_board.SetScore(score);
    }

    #region Game_Process_Interface
    public void GameReady() {
        m_state = Target_State.FLASHING;
    }

    public void GameStart() {
        m_state = Target_State.STATE1;
        m_target.GetComponent<Object_Flash>().StopLoop();
        m_target.GetComponent<Collider>().enabled = true;
        // Invoke("InverseSpeed", m_inverse_time);
    }

    public void GameEnd() {

    }

    public void GameEndBuffer() {

    }
    #endregion 
}
