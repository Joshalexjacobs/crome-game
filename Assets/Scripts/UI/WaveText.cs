using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveText : MonoBehaviour {

    private TextMesh text;
    private MeshRenderer mesh;

    // Use this for initialization
    void Start() {
        text = GetComponent<TextMesh>();
        mesh = GetComponent<MeshRenderer>();
        StartCoroutine("Blink");
    }

    public void StartBlinking(int phase) {
        int actualPhase = phase + 1;
        text.text = "WAVE " + actualPhase.ToString();
        StartCoroutine("Blink");
    }

    IEnumerator Blink() {
        for (int i = 0; i < 5; i++) {
            mesh.enabled = true;

            yield return new WaitForSeconds(0.25f);

            mesh.enabled = false;

            yield return new WaitForSeconds(0.25f);
        }

        mesh.enabled = false;
    }
}
