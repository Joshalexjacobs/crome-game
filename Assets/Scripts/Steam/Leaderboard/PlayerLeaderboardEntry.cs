using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeaderboardEntry {

    private string playerName;
    private int playerScore;
    private int position;

    public PlayerLeaderboardEntry(string playerName, int playerScore) {
        Debug.Log(playerName + " " + playerScore);

        this.playerName = playerName;
        this.playerScore = playerScore;
    }

    public PlayerLeaderboardEntry(string playerName, int playerScore, int position) {
        Debug.Log(position + " " + playerName + " " + playerScore);

        this.playerName = playerName;
        this.playerScore = playerScore;
        this.position = position;
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

    public void SetPosition(int position) {
        this.position = position;
    }

    public int GetPosition() {
        return position;
    }

}
