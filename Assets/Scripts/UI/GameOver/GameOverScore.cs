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

        mr.sortingLayerName = "Menu";
        mr.sortingOrder = 15;
    }

    public void SetScore(string text) {
        textMesh.text = text;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
