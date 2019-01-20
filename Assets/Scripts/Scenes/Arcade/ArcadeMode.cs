using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArcadeMode : MonoBehaviour {

    public int phase = 1;
    public WaveManager waveManager;

    public CanvasScaler canvasScaler;

    public GameOver gameOver;

    private AudioSource[] audio;
    private float waveLoopTime = 0.25f;

    private bool isGameOver = false;

    void Start () {
        audio = GetComponents<AudioSource>();

        int lastTrack = PlayerPrefs.GetInt("LastTrack");

        if (lastTrack != null && lastTrack == 0) {
            audio[1].Play();
            PlayerPrefs.SetInt("LastTrack", 1);
        } else {
            audio[0].Play();
            PlayerPrefs.SetInt("LastTrack", 0);
        }


        StartCoroutine("Arcade");
	}
	
    IEnumerator Arcade() {
        yield return new WaitForSeconds(2f);

        while(!isGameOver) {
            for (int i = 0; i < 5 && !isGameOver; i++) {
                waveManager.StartNewWave(phase);

                while (waveManager.layersCompleted < phase) {
                    yield return new WaitForSeconds(waveLoopTime);
                }

                ResetWaveManager();
            }

            if(!isGameOver) {
                waveManager.StartDeathCometWave(phase);
            }

            while (!waveManager.deathCometDead && !isGameOver) {
                yield return new WaitForSeconds(waveLoopTime);
            }

            ResetWaveManager();

            phase++;

            SteamAchievements steamAchievements = GameObject.FindObjectOfType<SteamAchievements>();
            if(steamAchievements != null) {
                steamAchievements.SetFurthestWave(phase);
            }
        }
    }

    private void ResetWaveManager() {
        waveManager.layersCompleted = 0;
        waveManager.deathCometDead = false;
    }

    public void HandleGameOver(string score) {
        isGameOver = true;
        gameOver.StartGameOverFadeIn(score);
    }

}