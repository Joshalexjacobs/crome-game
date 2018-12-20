﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour {

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
    }

    public void StartFadeIn() {
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn() {
        for (int i = 0; i <= 10; i++) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void StartFadeOut() {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut() {
        for (int i = 4; i >= 0; i--) {
            sr.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(0.75f);
        }
    }
}