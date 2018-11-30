using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverRank : MonoBehaviour {

    private MeshRenderer mr;
    private TextMesh text;

	void Start () {
        mr = GetComponent<MeshRenderer>();
        text = GetComponent<TextMesh>();

        mr.sortingLayerName = "Menu";
        mr.sortingOrder = 15;
    }
	
    public void Init(string score, bool playerScore) {
        if(text == null) {
            text = GetComponent<TextMesh>();
        }

        text.text = score;

        if(!playerScore) {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
}
