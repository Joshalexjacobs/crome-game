using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    private static int MULTIPLIER_BOOST = 10;
    private static float MULTIPLIER_MAX = 3f;

    //private Text text;
    private TextMesh text;

    private int playerScore = 0;
    private int maxStringLength = 6;

    private int multiplierProgress = 0;
    private float multiplier = 1f;

    void Start () {
        text = GetComponent<TextMesh>();
    }

    private void AppendZerosToScore() {
        int stringDifference = maxStringLength - playerScore.ToString().Length;
        for (int i = 0; i < stringDifference; i++) {
            text.text = 0f.ToString() + text.text;
        }
    }

    public void AddToScore(int x) {
        int mulitpliedScore = (int)Mathf.Round((float)x * multiplier);

        playerScore += (int) mulitpliedScore;

        text.text = playerScore.ToString();
        AppendZerosToScore();
    }

    public void AddToMultiplier(int x) {
        multiplierProgress += x;

        if (multiplierProgress >= MULTIPLIER_BOOST) {
            multiplierProgress = multiplierProgress - MULTIPLIER_BOOST;
            if(multiplier < MULTIPLIER_MAX) {
                multiplier += 0.25f;

                string multiplierString = "x " + multiplier;

                if (multiplier == 1.5f || multiplier == 2.5f) {
                    multiplierString += "0";
                } else if (multiplier == 2f) {
                    multiplierString += ".00";
                }

                if(multiplier == MULTIPLIER_MAX) {
                    multiplierString = "MAX";
                }

                MultiplierText multiplierText = GameObject.FindObjectOfType<MultiplierText>();
                multiplierText.StartFlashing(multiplierString);
            }
        }
    }

    public void ResetMultiplier() {
        multiplier = 1f;
        multiplierProgress = 0;
    }

    public int GetActualScore() {
        return playerScore;
    }

    public string GetTextScore() {
        return text.text;
    }

    public void PostScore() {
        if(SteamLeaderboards.IsReady()) {
            Debug.Log("Updating Score to... " + playerScore);
            SteamLeaderboards.UpdateScore(playerScore);
        }
    }
}
