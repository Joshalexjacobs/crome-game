using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleElement : MonoBehaviour {

    public bool isFlashing = false;
    public bool skipped = false;
    public float flashRate = 0.4f;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
    public void Skip() {
        if (sr == null) {
            sr = GetComponent<SpriteRenderer>();
        }

        skipped = true;
        sr.color = new Color(1f, 1f, 1f, 0f);
    }

    public void StartFadeIn() {
        StartCoroutine("FadeSRIn");
    }

    IEnumerator FadeSRIn() {
        if(sr == null) {
            sr = GetComponent<SpriteRenderer>();
        }

        for(int i = 0; i < 5 && !skipped; i++) {
            sr.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(0.1f);
        }

        if(isFlashing) {
            StartCoroutine("StartFlashing");
        }
    }

    public void StartFadeOut(float waitTime = 0.1f) {
        StartCoroutine("FadeSROut", waitTime);
    }

    IEnumerator FadeSROut(float waitTime) {
        isFlashing = false;

        if (sr == null) {
            sr = GetComponent<SpriteRenderer>();
        }

        for (int i = 4; i >= 0 && !skipped; i--) {
            sr.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator StartFlashing() {
        while(isFlashing && !skipped) {
            sr.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(0.4f);
            sr.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.4f);
        }
    }
}
