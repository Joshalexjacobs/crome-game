using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTextMesh : MonoBehaviour {

    private TextMesh text;

    // Use this for initialization
    void Start () {
        text = GetComponent<TextMesh>();
    }

    public void StartFadeIn() {
        StartCoroutine("FadeTextIn");
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

    public void StartFadeOut(float waitTime = 0.1f) {
        StartCoroutine("FadeTextOut", waitTime);
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
}
