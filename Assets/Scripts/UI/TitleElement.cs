using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleElement : MonoBehaviour {

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
    public void StartFadeIn() {
        StartCoroutine("FadeSRIn");
    }

    IEnumerator FadeSRIn() {
        if(sr == null) {
            sr = GetComponent<SpriteRenderer>();
        }

        for(int i = 0; i < 5; i++) {
            sr.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void StartFadeOut() {
        StartCoroutine("FadeSROut");
    }

    IEnumerator FadeSROut() {
        if (sr == null) {
            sr = GetComponent<SpriteRenderer>();
        }

        for (int i = 4; i >= 0; i--) {
            sr.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
