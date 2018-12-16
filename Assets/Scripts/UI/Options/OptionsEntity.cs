using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsEntity : MonoBehaviour {

    public bool isASlider = false;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
    }

    public void StartFadeIn() {
        StartCoroutine("FadeIn");
        FadeInChildren();
    }

    IEnumerator FadeIn() {
        for (int i = 0; i <= 10; i++) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void FadeInChildren() {
        OptionsSlider optionsSlider = GetComponentInChildren<OptionsSlider>();
        if(optionsSlider != null) {
            optionsSlider.StartFadeIn();
        }
    }

    public void StartFadeOut() {
        StartCoroutine("FadeOut");
        FadeOutChildren();
    }

    IEnumerator FadeOut() {
        for (int i = 10; i >= 0; i--) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void FadeOutChildren() {
        OptionsSlider optionsSlider = GetComponentInChildren<OptionsSlider>();
        if (optionsSlider != null) {
            optionsSlider.StartFadeOut();
        }
    }

    public virtual void HandleEntity() {
        Debug.Log("HandleOptionsEntity");
    }

    public virtual OptionsSlider GetSlider() {
        return null;
    }
}
