using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class SteamScript : MonoBehaviour {

    private LeaderboardTicker leaderboardTicker;

    // Use this for initialization
    void Start () {
        if (SteamManager.Initialized) {
            SteamLeaderboards.Init();
            StartCoroutine("GetTopThreeScores");
        }
    }

    IEnumerator GetTopThreeScores() {
        while(!SteamLeaderboards.IsReady()) {
            yield return new WaitForSeconds(0.5f);
        }

        SteamLeaderboards.GetTopThreeEntriesInLeaderboard();
    }

    public void SetTopThreePlayers(ArrayList topThreePlayers) {
        int index = 0;
        index.ToString();

        string[] players = new string[3];

        foreach(PlayerLeaderboardEntry entry in topThreePlayers) {
            string playerName = entry.GetPlayerName();

            if (playerName.Length > 19) {
                playerName = playerName.Substring(0, 18) + ".";
            }
            
            players[index] = (index + 1).ToString() + ". " + playerName + " - " + entry.GetPlayerScore().ToString() + " ";
            index++;
        }

        if(leaderboardTicker != null) {
            leaderboardTicker.SetLeaderboardTicker(players);
        } else {
            leaderboardTicker = GameObject.FindObjectOfType<LeaderboardTicker>();
            leaderboardTicker.SetLeaderboardTicker(players);
        }
        
    }
}
