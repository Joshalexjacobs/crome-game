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

    protected Callback<UserStatsReceived_t> userStatsRecieved;
    protected Callback<UserStatsStored_t> userStatsStored;
    protected Callback<UserAchievementStored_t> userAchievementStored;

    private void OnEnable() {
        if(!SteamManager.Initialized) {
            return;
        }

        cGameID = new CGameID(SteamUtils.GetAppID());

        userStatsRecieved = Callback<UserStatsReceived_t>.Create(OnUserStatsRecieved);
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

    //public void OnGameStateChange() {
    //    if(!statsValid) {
    //        return;
    //    }
    //}

    private void UnlockAchievement(Achievement achievement) {
        achievement.achieved = true;
        SteamUserStats.SetAchievement(achievement.achievementId.ToString());
        storeStats = true;
    }

    private void OnUserStatsRecieved(UserStatsReceived_t pCallback) {

    }

    private void OnUserStatsStored(UserStatsStored_t pCallback) {

    }

    private void OnAchievementStored(UserAchievementStored_t pCallback) {

    }

    public void Render() {

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
