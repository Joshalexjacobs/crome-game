using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    private GameOverScore gameOverScore;

	// Use this for initialization
	void Start () {
        gameOverScore = GetComponentInChildren<GameOverScore>();
    }

    public void StartGameOverFadeIn() {

    }

	// Update is called once per frame
	void Update () {
		
	}
}
