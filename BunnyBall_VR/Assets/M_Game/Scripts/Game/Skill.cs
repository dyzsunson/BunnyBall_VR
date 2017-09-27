using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {
    public bool is_working = false;
    public float timeLeft = 0.0f;
    public float timeTotal = 5.0f;

    public bool is_Reloading = false;
    public float reloadTimeLeft = 0.0f;
    public float reloadTimeTotal = 10.0f;

    public KeyCode hotKey = KeyCode.Q;
    public KeyCode joyHotKey = KeyCode.Joystick1Button1;

    public GameObject UIObj;

    public static bool Any_skill_running = false;

    private void Start() {
        Any_skill_running = false;
    }

    void Update() {
        if (SceneController.Ellic_Current.ShootCtrl.Able_Fire) {
            // active a skill
            if (!Any_skill_running) {

                if (!this.is_Reloading && (InputCtrl.IsHotKeyDown(hotKey) || InputCtrl.IsHotKeyDown(joyHotKey))) {
                    this.is_working = true;
                    Any_skill_running = true;

                    this.UIObj.transform.Find("Active").gameObject.SetActive(true);
                    this.timeLeft = this.timeTotal;
                }

            }

            // running

            if (this.is_working) {
                this.timeLeft -= Time.deltaTime;
                if (this.timeLeft < 0.0f) {
                    this.is_working = false;
                    Any_skill_running = false;
                    this.UIObj.transform.Find("Active").gameObject.SetActive(false);

                    this.is_Reloading = true;
                    this.reloadTimeLeft = this.reloadTimeTotal;
                }
            }


            // reloading
            if (this.is_Reloading) {
                this.reloadTimeLeft -= Time.deltaTime;
                if (this.reloadTimeLeft <= 0.0f) {
                    this.is_Reloading = false;
                    this.reloadTimeLeft = 0.0f;
                }
                this.UIObj.transform.Find("Mask").GetComponent<RectTransform>().localScale =
                    new Vector3(1.0f, this.reloadTimeLeft / this.reloadTimeTotal, 1.0f);
            }
        }
    }
}
