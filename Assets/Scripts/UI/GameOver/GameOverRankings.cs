using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverRankings : MonoBehaviour {

    public GameObject ranking;
    private int playerEntry = 0;
    private static Vector3 RANKING_VECTOR = new Vector3(0.015f, 0.0f, 1f);
    private static float RANKING_DIFFERENCE_Y = -0.09f;

	// Use this for initialization
	void Start () {
		
	}
	
    public void GetPlayerEntry() {
        SteamLeaderboards.GetPlayerEntry(gameObject);
    }

    public void PassBackPlayerEntry(int playerEntry) {
        this.playerEntry = playerEntry;
        SteamLeaderboards.GetSurroundingEntries(playerEntry, gameObject);
    }

    public void PassBackSurroundingEntries(ArrayList results) {
        StartCoroutine("ParseSurroundingEntryResults", results);
    }

    IEnumerator ParseSurroundingEntryResults(ArrayList results) {
        yield return new WaitForSeconds(1f);

        int index = 0;
        foreach (PlayerLeaderboardEntry entry in results) {
            Vector3 difference = new Vector3(RANKING_VECTOR.x, RANKING_DIFFERENCE_Y * index, RANKING_VECTOR.z);

            GameOverRank rankingObj = Instantiate(ranking, transform.position + difference, Quaternion.identity).GetComponent<GameOverRank>();
            rankingObj.transform.parent = gameObject.transform;

            string rankingText = entry.GetPosition() + ". " + entry.GetPlayerName() + " - " + entry.GetPlayerScore();
            rankingObj.Init(rankingText, entry.GetPosition() == this.playerEntry ? true : false);
            index++;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
