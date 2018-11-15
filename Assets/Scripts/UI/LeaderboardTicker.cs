﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardTicker : MonoBehaviour {

    private TextMesh text;
    private string [] players = new string[3];

	// Use this for initialization
	void Start () {
        text = GetComponent<TextMesh>();
	}

    public void StartFadeIn() {
        StartCoroutine("Switch");
    }

    IEnumerator FadeTextIn() {
        if (text == null) {
            text = GetComponent<TextMesh>();
        }

        for (int i = 0; i < 5; i++) {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0.25f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Switch() {
        while(players[0].Length > 0) {
            foreach(string playerScore in players) {
                if(playerScore != null && playerScore.Length > 0) {
                    text.text = playerScore;
                    StartCoroutine("FadeTextIn");
                    yield return new WaitForSeconds(3.5f);
                    StartCoroutine("FadeTextOut", 0.05f);
                    yield return new WaitForSeconds(.25f);
                }
            }
        }
    }

    public void StartFadeOut(float waitTime = 0.1f) {
        Destroy(gameObject);
        //StartCoroutine("FadeTextOut", waitTime);
    }

    IEnumerator FadeTextOut(float waitTime) {
        if (text == null) {
            text = GetComponent<TextMesh>();
        }

        for (int i = 4; i >= 0; i--) {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0.25f * i);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void SetLeaderboardTicker(string [] players) {
        this.players = players;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
