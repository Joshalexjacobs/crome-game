﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCursor : MonoBehaviour {

    public Vector2 retryPosition;
    public Vector2 exitPosition;

    private SpriteRenderer sr;
    private bool isActive = false;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
    }

    public void StartFadeIn() {
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn() {
        for (int i = 0; i < 10; i++) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
