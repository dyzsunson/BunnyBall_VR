using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class FadeLoop : MonoBehaviour {
    public CanvasGroup[] groupList;

    CanvasGroup group;
    float alphaNow = 0.0f;
    float totalTime;

    public float fadeInTime = 1.0f;
    public float fadeOutTime = 1.0f;
    public float waitTime = 1.0f;

    bool isWorking = false;
    public bool startFloop = false;

    enum FadeType {
        fadeIn,
        wait,
        fadeOut
    };

    FadeType fadeType;

    void Awake() {
        group = this.GetComponent<CanvasGroup>();
        group.alpha = 0f;
    }
    // Use this for initialization
    void Start() {
        if (startFloop)
            StartLoop();
    }

    public void StartLoop(float _fadeInTime, float _fadeOutTime, float _waitTime) {
        fadeInTime = _fadeInTime;
        fadeOutTime = _fadeOutTime;
        waitTime = _waitTime;
        StartLoop();
    }

    public void StartLoop() {
        if (!isWorking) {
            isWorking = true;
            FadeIn(this.fadeInTime);
        }
    }

    public void StopLoop() {
        isWorking = false;
        group.alpha = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        if (isWorking) {
            alphaNow += Time.deltaTime;
            if (alphaNow > totalTime) {
                if (fadeType == FadeType.fadeIn) {
                    Wait(this.waitTime);
                }
                else if (fadeType == FadeType.wait) {
                    FadeOut(this.fadeOutTime);
                }
                else {
                    FadeIn(this.fadeInTime);
                }
            }


            switch (fadeType) {
                case FadeType.fadeIn:
                    group.alpha = getEnterAlpha(alphaNow / totalTime);
                    break;
                case FadeType.fadeOut:
                    group.alpha = getOutAlpha(alphaNow / totalTime);
                    break;
            }

            foreach (CanvasGroup g in groupList) {
                g.alpha = group.alpha;
            }
        }
    }

    void FadeIn(float time) {
        totalTime = time;
        alphaNow = 0.0f;
        fadeType = FadeType.fadeIn;
    }

    void Wait(float time) {
        totalTime = time;
        fadeType = FadeType.wait;
    }

    void FadeOut(float time) {
        totalTime = time;
        alphaNow = 0.0f;
        fadeType = FadeType.fadeOut;
    }

    float getEnterAlpha(float percentage) {
        float x = (percentage * 2f - 1f) * 5.0f;
        float alpha = 1f / (1f + Mathf.Pow(Mathf.Exp(1f), -x));
        return alpha;
    }

    float getOutAlpha(float percentage) {
        return 1f - getEnterAlpha(percentage);
    }
}
