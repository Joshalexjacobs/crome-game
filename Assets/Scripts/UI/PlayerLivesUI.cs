using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLivesUI : MonoBehaviour {

    private TextMesh text;
    private int lives;
	
	void Start () {
        text = GetComponent<TextMesh>();
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
