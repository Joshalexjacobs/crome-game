using UnityEngine;
using Steamworks;
using System.Collections;
using System.Threading;

public class SteamLeaderboards : MonoBehaviour {
    private const string LEADERBOARD_NAME = "Global Leaderboard";
    private const ELeaderboardUploadScoreMethod LEADERBOARD_UPLOAD_METHOD = ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest;
    private const ELeaderboardDataRequest LEADERBOARD_DATA_REQUEST = ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal;

    private static SteamLeaderboard_t currentLeaderboard;
    private static bool isReady = false;
    private static CallResult<LeaderboardScoresDownloaded_t> topThreeResult = new CallResult<LeaderboardScoresDownloaded_t>();
    private static CallResult<LeaderboardFindResult_t> findResult = new CallResult<LeaderboardFindResult_t>();
    private static CallResult<LeaderboardScoreUploaded_t> uploadResult = new CallResult<LeaderboardScoreUploaded_t>();

    public static void Init() {
        SteamAPICall_t hSteamAPICall = SteamUserStats.FindLeaderboard(LEADERBOARD_NAME);

        findResult.Set(hSteamAPICall, (pCallback, failure) => {
            UnityEngine.Debug.Log("STEAM LEADERBOARDS: Found - " + pCallback.m_bLeaderboardFound + " leaderboardID - " + pCallback.m_hSteamLeaderboard.m_SteamLeaderboard);
            currentLeaderboard = pCallback.m_hSteamLeaderboard;
            isReady = true;
        });

        InitTimer();
    }

    public static bool IsReady() {
        return isReady;
    }

    public static void GetTopThreeEntriesInLeaderboard() {
        SteamAPICall_t hSteamAPICall = SteamUserStats.DownloadLeaderboardEntries(currentLeaderboard, LEADERBOARD_DATA_REQUEST, 1, 3);

        topThreeResult.Set(hSteamAPICall, (pCallback, failure) => {
            ArrayList results = new ArrayList();

            for(int i = 0; i < pCallback.m_cEntryCount; i++) {
                LeaderboardEntry_t leaderboardEntry;
                int[] details = new int[1];
                SteamUserStats.GetDownloadedLeaderboardEntry(pCallback.m_hSteamLeaderboardEntries, i, out leaderboardEntry, details, 1);

                if (leaderboardEntry.m_steamIDUser.m_SteamID > 0) {
                    PlayerLeaderboardEntry playerLeaderboardEntry = new PlayerLeaderboardEntry(GetUserName(leaderboardEntry.m_steamIDUser), leaderboardEntry.m_nScore);
                    results.Insert(i, playerLeaderboardEntry);
                }
            }

            GameObject.FindObjectOfType<SteamScript>().SetTopThreePlayers(results);
        });
    }

    private static string GetUserName(CSteamID steamID) {
        Debug.Log("GetUserName - " + SteamFriends.GetFriendPersonaName(steamID));
        return SteamFriends.GetFriendPersonaName(steamID);
    }

    public static void UpdateScore(int score) {
        if (!isReady) {
            Debug.Log("Error uploading score to steam leaderboard " + LEADERBOARD_NAME);
        } else {
            UnityEngine.Debug.Log("uploading score (" + score + ") to steam leaderboard (" + LEADERBOARD_NAME + ")");
            SteamAPICall_t hSteamAPICall = SteamUserStats.UploadLeaderboardScore(currentLeaderboard, LEADERBOARD_UPLOAD_METHOD, score, null, 0);
            uploadResult.Set(hSteamAPICall, OnLeaderboardUploadResult);
        }
    }

    static private void OnLeaderboardFindResult(LeaderboardFindResult_t pCallback, bool failure) {
        UnityEngine.Debug.Log("STEAM LEADERBOARDS: Found - " + pCallback.m_bLeaderboardFound + " leaderboardID - " + pCallback.m_hSteamLeaderboard.m_SteamLeaderboard);
        currentLeaderboard = pCallback.m_hSteamLeaderboard;
        isReady = true;
    }

    static private void OnLeaderboardUploadResult(LeaderboardScoreUploaded_t pCallback, bool failure) {
        UnityEngine.Debug.Log("STEAM LEADERBOARDS: failure - " + failure + " Completed - " + pCallback.m_bSuccess + " NewScore: " + pCallback.m_nGlobalRankNew + " Score " + pCallback.m_nScore + " HasChanged - " + pCallback.m_bScoreChanged);
    }



    private static Timer timer1;
    public static void InitTimer() {
        timer1 = new Timer(timer1_Tick, null, 0, 1000);
    }

    private static void timer1_Tick(object state) {
        SteamAPI.RunCallbacks();
    }
}