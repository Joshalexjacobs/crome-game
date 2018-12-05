using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierText : MonoBehaviour {

    TextMesh textMesh;

	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMesh>();
    }
	
    public void StartFlashing(string text) {
        textMesh.text = text;
        StartCoroutine("Flash"); // "x 1.25"
    }

    IEnumerator Flash() {
        for(int i = 0; i < 5; i++) {
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0f);
            yield return new WaitForSeconds(0.25f);
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1f);
            yield return new WaitForSeconds(0.25f);
        }

        textMesh.text = "";
    }
}
