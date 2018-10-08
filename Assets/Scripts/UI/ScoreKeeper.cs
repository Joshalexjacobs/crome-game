using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    private Text text;

    private int playerScore = 0;
    private int maxStringLength = 6;

    void Start () {
        text = GetComponent<Text>();
    }

    private void AppendZerosToScore() {
        int stringDifference = maxStringLength - playerScore.ToString().Length;
        for (int i = 0; i < stringDifference; i++) {
            text.text = 0f.ToString() + text.text;
        }
    }

    public void AddToScore(int x) {
        playerScore += x;
        text.text = playerScore.ToString();
        AppendZerosToScore();
        //PlayerPrefs.SetString("mostRecentScore", text.text);
    }
}
