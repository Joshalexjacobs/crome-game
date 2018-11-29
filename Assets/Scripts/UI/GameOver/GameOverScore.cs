using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScore : MonoBehaviour {

    private MeshRenderer mr;
    private TextMesh textMesh;

	// Use this for initialization
	void Start () {
        mr = GetComponent<MeshRenderer>();
        textMesh = GetComponent<TextMesh>();

        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0f);

        mr.sortingLayerName = "Menu";
        mr.sortingOrder = 15;
    }

    public void SetScore(string text) {
        textMesh.text = text;
    }

    public void StartFadeIn() {
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn() {
        for (int i = 0; i < 10; i++) {
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
