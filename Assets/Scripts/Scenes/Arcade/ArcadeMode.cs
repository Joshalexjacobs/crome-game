using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeMode : MonoBehaviour {

    public int phase = 1;
    public WaveManager waveManager;

    private float waveLoopTime = 0.25f;

    void Start () {
        // changing the resolution...
        // Screen.SetResolution(480, 432, false); // set the resolution
        // set canvas scale factor to appropriate number (160 x 144 = 1, 320 x 288 = 2, 480 x 432 = 3 etc...)
        // make sure pixel perfect camera is set to 160 x 144 and upscale render texture is selected

        StartCoroutine("Arcade");
	}
	
    IEnumerator Arcade() {
        yield return new WaitForSeconds(2f);

        while(true) {
            for (int i = 0; i < 10; i++) {
                waveManager.StartNewWave(phase);

                while (waveManager.layersCompleted < phase) {
                    yield return new WaitForSeconds(waveLoopTime);
                }

                ResetWaveManager();
            }

            waveManager.StartDeathCometWave(phase);

            while (waveManager.layersCompleted < phase) {
                yield return new WaitForSeconds(waveLoopTime);
            }

            ResetWaveManager();

            phase++;
        }
    }

    private void ResetWaveManager() {
        waveManager.layersCompleted = 0;
    }

	void Update () {
        if (Input.GetKey("escape")) {
            SceneManager.LoadScene("title");
        }
    }

}