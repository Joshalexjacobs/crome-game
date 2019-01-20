using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamAchievements : MonoBehaviour {

    //https://github.com/rlabrecque/Steamworks.NET-Example/blob/master/Assets/Scripts/SteamStatsAndAchievements.cs

    private enum AchievementID : int {
        FIRST_BLOOD,
        CONTENDER,
        EXECUTIONER,
        HEADSMAN,
        IT_TOLLS_FOR_THEE,
        NEW_CHALLENGER,
        PROFESSIONAL,
        STRAPPED,
        SUPER_CHARGED
    }

    private Achievement[] achievements = new Achievement[] {
        new Achievement(AchievementID.FIRST_BLOOD, "First Blood", ""),
        new Achievement(AchievementID.CONTENDER, "Contender", ""),
        new Achievement(AchievementID.EXECUTIONER, "Executioner", ""),
        new Achievement(AchievementID.HEADSMAN, "Headsman", ""),
        new Achievement(AchievementID.IT_TOLLS_FOR_THEE, "It tolls for thee", ""),
        new Achievement(AchievementID.NEW_CHALLENGER, "New Challenger", ""),
        new Achievement(AchievementID.PROFESSIONAL, "Professional", ""),
        new Achievement(AchievementID.STRAPPED, "Strapped", ""),
        new Achievement(AchievementID.SUPER_CHARGED, "Super Charged", "")
    };

    private CGameID cGameID;

    private bool requestedStats;
    private bool statsValid;

    private bool storeStats;

    private int deaths;
    private int kills;
    private int furthestWave;
    private int strapped;
    private int superCharged;

    protected Callback<UserStatsReceived_t> userStatsReceived;
    protected Callback<UserStatsStored_t> userStatsStored;
    protected Callback<UserAchievementStored_t> userAchievementStored;

    private void OnEnable() {
        if(!SteamManager.Initialized) {
            return;
        }

        //SteamUserStats.ResetAllStats(true);

        cGameID = new CGameID(SteamUtils.GetAppID());

        userStatsReceived = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
        userStatsStored = Callback<UserStatsStored_t>.Create(OnUserStatsStored);
        userAchievementStored = Callback<UserAchievementStored_t>.Create(OnAchievementStored);

        requestedStats = false;
        statsValid = false;
    }

    private void Update() {
        if(!SteamManager.Initialized) {
            return;
        }

        if(!requestedStats) {
            if (!SteamManager.Initialized) {
                requestedStats = true;
                return;
            }

            bool success = SteamUserStats.RequestCurrentStats();

            requestedStats = success;
        }

        if(!statsValid) {
            return;
        }

        foreach(Achievement achievement in achievements) {
            if(achievement.achieved) {
                continue; 
            }

            switch(achievement.achievementId) {
                case AchievementID.FIRST_BLOOD:
                    if(deaths > 0) {
                        UnlockAchievement(achievement);
                    }
                    break;
                case AchievementID.CONTENDER:
                    if(furthestWave >= 5) {
                        UnlockAchievement(achievement);
                    }
                    break;
                case AchievementID.EXECUTIONER:
                    if (kills >= 1000) {
                        UnlockAchievement(achievement);
                    }
                    break;
                case AchievementID.HEADSMAN:
                    if (kills >= 500) {
                        UnlockAchievement(achievement);
                    }
                    break;
                case AchievementID.IT_TOLLS_FOR_THEE:
                    if (deaths >= 100) {
                        UnlockAchievement(achievement);
                    }
                    break;
                case AchievementID.NEW_CHALLENGER:
                    if (furthestWave >= 3) {
                        UnlockAchievement(achievement);
                    }
                    break;
                case AchievementID.PROFESSIONAL:
                    if (furthestWave >= 8) {
                        UnlockAchievement(achievement);
                    }
                    break;
                case AchievementID.STRAPPED:
                    if(strapped > 0) {
                        UnlockAchievement(achievement);
                    }
                    break;
                case AchievementID.SUPER_CHARGED:
                    if (superCharged > 0) {
                        UnlockAchievement(achievement);
                    }
                    break;
            }

            if(storeStats) {
                SteamUserStats.SetStat("deaths", deaths);
                SteamUserStats.SetStat("kills", kills);
                SteamUserStats.SetStat("furthestWave", furthestWave);
                SteamUserStats.SetStat("strapped", strapped);
                SteamUserStats.SetStat("superCharged", superCharged);

                bool success = SteamUserStats.StoreStats();

                storeStats = !success;
            }

        }
    }

    public void AddDeaths(int death) {
        this.deaths += death;
    }

    public void AddKills(int kills) {
        this.kills += kills;
    }

    public void SetFurthestWave(int wave) {
        if(wave > furthestWave) {
            furthestWave = wave;
        }
    }

    public void SetStrapped() {
        strapped = 1;
    }

    public void SetSuperCharged() {
        superCharged = 1;
    }

    public void OnGameStateChange() {
        if(!statsValid) {
            return;
        }

        storeStats = true;
    }

    private void UnlockAchievement(Achievement achievement) {
        achievement.achieved = true;
        SteamUserStats.SetAchievement(achievement.achievementId.ToString());
        storeStats = true;
    }

    private void OnUserStatsReceived(UserStatsReceived_t pCallback) {
        if(!SteamManager.Initialized) {
            return;
        }

        if ((ulong)cGameID == pCallback.m_nGameID) {
            if (EResult.k_EResultOK == pCallback.m_eResult) {
                Debug.Log("Received stats and achievements from Steam\n");

                statsValid = true;

                // load achievements
                foreach (Achievement ach in achievements) {
                    bool ret = SteamUserStats.GetAchievement(ach.achievementId.ToString(), out ach.achieved);
                    if (ret) {
                        ach.name = SteamUserStats.GetAchievementDisplayAttribute(ach.achievementId.ToString(), "name");
                        ach.description = SteamUserStats.GetAchievementDisplayAttribute(ach.achievementId.ToString(), "desc");
                    } else {
                        Debug.LogWarning("SteamUserStats.GetAchievement failed for Achievement " + ach.achievementId + "\nIs it registered in the Steam Partner site?");
                    }
                }

                // load stats
                SteamUserStats.GetStat("deaths", out deaths);
                SteamUserStats.GetStat("kills", out kills);
                SteamUserStats.GetStat("furthestWave", out furthestWave);
                SteamUserStats.GetStat("strapped", out strapped);
                SteamUserStats.GetStat("superCharged", out superCharged);
            } else {
                Debug.Log("RequestStats - failed, " + pCallback.m_eResult);
            }
        }
    }

    private void OnUserStatsStored(UserStatsStored_t pCallback) {
        // we may get callbacks for other games' stats arriving, ignore them
        if ((ulong)cGameID == pCallback.m_nGameID) {
            if (EResult.k_EResultOK == pCallback.m_eResult) {
                Debug.Log("StoreStats - success");
            } else if (EResult.k_EResultInvalidParam == pCallback.m_eResult) {
                // One or more stats we set broke a constraint. They've been reverted,
                // and we should re-iterate the values now to keep in sync.
                Debug.Log("StoreStats - some failed to validate");
                // Fake up a callback here so that we re-load the values.
                UserStatsReceived_t callback = new UserStatsReceived_t();
                callback.m_eResult = EResult.k_EResultOK;
                callback.m_nGameID = (ulong)cGameID;
                OnUserStatsReceived(callback);
            } else {
                Debug.Log("StoreStats - failed, " + pCallback.m_eResult);
            }
        }
    }

    private void OnAchievementStored(UserAchievementStored_t pCallback) {
        // We may get callbacks for other games' stats arriving, ignore them
        if ((ulong)cGameID == pCallback.m_nGameID) {
            if (0 == pCallback.m_nMaxProgress) {
                Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' unlocked!");
            } else {
                Debug.Log("Achievement '" + pCallback.m_rgchAchievementName + "' progress callback, (" + pCallback.m_nCurProgress + "," + pCallback.m_nMaxProgress + ")");
            }
        }
    }

    public void OnGUI() {
        if (!SteamManager.Initialized) {
            GUILayout.Label("Steamworks not Initialized");
            return;
        }

        GUILayout.BeginArea(new Rect(0, 0, 160, 144));

        GUILayout.Label("deaths: " + deaths);
        GUILayout.Space(1);
        GUILayout.Label("kills: " + kills);
        GUILayout.Space(1);
        GUILayout.Label("furthestWave: " + furthestWave);
        GUILayout.Space(1);
        GUILayout.Label("strapped: " + strapped);
        GUILayout.Space(1);
        GUILayout.Label("superCharged: " + superCharged);

        //GUILayout.BeginArea(new Rect(Screen.width - 300, 0, 300, 800));
        //foreach (Achievement ach in achievements) {
        //    GUILayout.Label(ach.achievementId.ToString());
        //    GUILayout.Label(ach.name + " - " + ach.description);
        //    GUILayout.Label("Achieved: " + ach.achieved);
        //    GUILayout.Space(20);
        //}

        // FOR TESTING PURPOSES ONLY!
        //if (GUILayout.Button("RESET STATS AND ACHIEVEMENTS")) {
        //    SteamUserStats.ResetAllStats(true);
        //    SteamUserStats.RequestCurrentStats();
        //    OnGameStateChange();
        //}

        GUILayout.EndArea();
    }

    private class Achievement {
        public AchievementID achievementId;
        public string name;
        public string description;
        public bool achieved;

        public Achievement(AchievementID achievementID, string name, string description) {
            this.achievementId = achievementID;
            this.name = name;
            this.description = description;
            achieved = false;
        }
    }
}
