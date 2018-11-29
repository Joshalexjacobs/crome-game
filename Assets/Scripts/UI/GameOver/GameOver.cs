using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    private GameOverScore gameOverScore;
    private GameOverBG gameOverBG;
    private GameOverCursor gameOverCursor;

    void Start() {
        GetGameOverObjects();
    }

    private void GetGameOverObjects() {
        gameOverScore = GetComponentInChildren<GameOverScore>();
        gameOverBG = GetComponentInChildren<GameOverBG>();
        gameOverCursor = GetComponentInChildren<GameOverCursor>();
    }

    public void StartGameOverFadeIn() {
        gameOverBG.StartFadeIn();
        gameOverScore.StartFadeIn();
        gameOverCursor.StartFadeIn();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
