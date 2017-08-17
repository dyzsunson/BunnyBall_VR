using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInOut : MonoBehaviour {
    CanvasGroup group;
    bool isWorking = false;
    float alphaNow = 0.0f;
    float totalTime;

    public bool isAutoStart = false;

    public float outAlpha = 0.0f;// = 0.1f;
    public float inAlpha = 1.0f;

    public float outScale = 1.0f;
    public float inScale = 1.0f;

    enum FadeType {
        fadeIn,
        fadeOut
    };

    FadeType fadeType;

    void Awake() {
        group = this.GetComponent<CanvasGroup>();
        group.alpha = outAlpha; // 0f;
    }
	// Use this for initialization
	void Start () { 

	}
	
	// Update is called once per frame
	void Update () {
        if (isWorking) {
            alphaNow += Time.deltaTime;
            if (alphaNow > totalTime)
                AnimationEnd();

            switch (fadeType) {
                case FadeType.fadeIn:
                    group.alpha = getEnterAlpha(alphaNow / totalTime);
                    this.transform.localScale = getEnterScale(alphaNow / totalTime) * Vector3.one;
                    break;
                case FadeType.fadeOut:
                    group.alpha = getOutAlpha(alphaNow / totalTime);
                    this.transform.localScale = getOutScale(alphaNow / totalTime) * Vector3.one;
                    break;
            }
        }

    }

    public void FadeIn(float time) {
        totalTime = time;
        group.alpha = alphaNow = 0.0f;
        isWorking = true;
        fadeType = FadeType.fadeIn;
    }

    public void FadeOut(float time) {
        totalTime = time;
        alphaNow = 0.0f;
        isWorking = true;
        fadeType = FadeType.fadeOut;
    }

    float getPercentage(float percentage) {
        float x = (percentage * 2f - 1f) * 5.0f;
        float alpha = 1f / (1f + Mathf.Pow(Mathf.Exp(1f), -x));
        return alpha;
    }

    float getEnterAlpha(float percentage) {
        return getPercentage(percentage) * (inAlpha - outAlpha) + outAlpha;
    }

    float getOutAlpha(float percentage) {
        return inAlpha - (inAlpha - outAlpha) * (getPercentage(percentage));
    }

    float getEnterScale(float percentage) {
        return getPercentage(percentage) * (inScale - outScale) + outScale;
    }

    float getOutScale(float percentage) {
        return inScale - (inScale - outScale) * (getPercentage(percentage));
    }

    void AnimationEnd() {
        isWorking = false;

        switch (fadeType) {
            case FadeType.fadeIn:
                group.alpha = inAlpha; // 1.0f;
                break;
            case FadeType.fadeOut:
                group.alpha = outAlpha; // 0.0f;
                break;
        }
    }
}
