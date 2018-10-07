using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLivesUI : MonoBehaviour {

    private Text text;
    private int lives;
	
	void Start () {
        text = GetComponent<Text>();
	}

    public void SetLives(int lives) {
        this.lives = lives;
    }

    public void DecrementLives() {
        lives--;
        text.text = "x" + lives.ToString();
    }

    public void IncrementLives() {
        lives++;
        text.text = "x" + lives.ToString();
    }
}
