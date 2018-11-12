using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveText : MonoBehaviour {

    private Text text;

    // Use this for initialization
    void Start() {
        text = GetComponent<Text>();
        text.enabled = false;
    }

    public void StartBlinking(int phase) {
        int actualPhase = phase + 1;
        text.text = "WAVE " + actualPhase.ToString();
        StartCoroutine("Blink");
    }

    IEnumerator Blink() {
        for (int i = 0; i < 5; i++) {
            text.enabled = true;

            yield return new WaitForSeconds(0.25f);

            text.enabled = false;

            yield return new WaitForSeconds(0.25f);
        }

        text.enabled = false;
    }
}
