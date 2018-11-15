using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpText : MonoBehaviour {

    private TextMesh text;
    private MeshRenderer mesh;

	// Use this for initialization
	void Start () {
        text = GetComponent<TextMesh>();
        mesh = GetComponent<MeshRenderer>();

        mesh.enabled = false;
	}
	
    public void StartBlinking(int level) {
        int actualLevel = level + 1;
        text.text = "LEVEL " + actualLevel.ToString();
        StartCoroutine("Blink");
    }

    IEnumerator Blink() {
        for (int i = 0; i < 3; i++)
        {
            mesh.enabled = true;

            yield return new WaitForSeconds(0.25f);

            mesh.enabled = false;

            yield return new WaitForSeconds(0.25f);
        }

        mesh.enabled = false;
    }
}
