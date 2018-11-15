using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeaderboardEntry {

    private string playerName;
    private int playerScore;

    public PlayerLeaderboardEntry(string playerName, int playerScore) {
        Debug.Log("this.playerName = " + playerName);
        Debug.Log("this.playerScore = " + playerScore);

        this.playerName = playerName;
        this.playerScore = playerScore;
    }

    public PlayerLeaderboardEntry() {

    }

    public void SetPlayerName(string playerName) {
        this.playerName = playerName;
    }

    public string GetPlayerName() {
        return playerName;
    }

    public void SetPlayerScore(int playerScore) {
        this.playerScore = playerScore;
    }

    public int GetPlayerScore() {
        return playerScore;
    }

}
