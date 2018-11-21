using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArcadeMode : MonoBehaviour {

    public int phase = 1;
    public WaveManager waveManager;

    public CanvasScaler canvasScaler;

    private AudioSource[] audio;
    private float waveLoopTime = 0.25f;

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

        while(true) {
            for (int i = 0; i < 5; i++) {
                waveManager.StartNewWave(phase);

                while (waveManager.layersCompleted < phase) {
                    yield return new WaitForSeconds(waveLoopTime);
                }

                ResetWaveManager();
            }

            waveManager.StartDeathCometWave(phase);

            while (!waveManager.deathCometDead) {
                yield return new WaitForSeconds(waveLoopTime);
            }

            ResetWaveManager();

            phase++;
        }
    }

    private void ResetWaveManager() {
        waveManager.layersCompleted = 0;
        waveManager.deathCometDead = false;
    }

	void Update () {
        if (Input.GetKey("escape")) {
            SceneManager.LoadScene("title");
        }
    }

}