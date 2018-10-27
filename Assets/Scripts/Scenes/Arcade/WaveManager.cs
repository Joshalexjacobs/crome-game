using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    // public vars

    public bool isTesting = false;
    public int testWave;

    // enemies

    public GameObject antEater;
    public GameObject chopper;

    public GameObject cyclopsFlyDown;
    public GameObject cyclopsLeft;
    public GameObject cyclopsRight;

    public GameObject fighter;
    public GameObject medusa;
    public GameObject mine;
    public GameObject shielder;
    public GameObject vulture;

    public GameObject deathCometSpawn;

    // other vars

    private float waveLoopTime = 0.25f;

    // Use this for initialization
    void Start () {
		if(isTesting) {
            StartCoroutine("TestWave");
        }
	}

    private bool LoopWhileAlive(params GameObject[] args) {
        foreach (GameObject arg in args) {
            if (arg) {
                return true;
            }
        }

        return false;
    }

    private bool LoopWhileAliveArray(GameObject[] args) {
        foreach (GameObject arg in args) {
            if (arg) {
                return true;
            }
        }

        return false;
    }

    private void HandleEndOfWave() {
        //isDone = true;
        //layersCompleted++;
    }

    IEnumerator TestWave() {
        yield return new WaitForSeconds(3f);
        StartCoroutine("Wave" + testWave.ToString());
    }

    IEnumerator Wave() {
        GameObject[] waveObjs = new GameObject[10];

        //waveObjs[0] = Instantiate(cyclopsRight, new Vector2(1f, 0.75f), Quaternion.identity);
        //waveObjs[1] = Instantiate(cyclopsLeft, new Vector2(-1f, 0.75f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave1() {
        GameObject cyclopsToSpawn = cyclopsLeft;
        float direction = 1;

        if (Random.Range(0, 2) == 0) {
            cyclopsToSpawn = cyclopsRight;
            direction = -1;
        }

        GameObject[] waveObjs = new GameObject[3];

        waveObjs[0] = Instantiate(cyclopsToSpawn, new Vector2(direction * 0.8f, 1.5f), Quaternion.identity);
        waveObjs[1] = Instantiate(cyclopsToSpawn, new Vector2(direction * 0.9f, 1.6f), Quaternion.identity);
        waveObjs[2] = Instantiate(cyclopsToSpawn, new Vector2(direction * 1f, 1.7f), Quaternion.identity);

        for (int i = 1; i < waveObjs.Length; i++) {
            waveObjs[i].GetComponent<Cyclops>().stopPoint -= 0.05f * i;
        }

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave2() {
        GameObject[] waveObjs = new GameObject[4];

        waveObjs[0] = Instantiate(cyclopsLeft, new Vector2(0.8f, 1.5f), Quaternion.identity);
        waveObjs[1] = Instantiate(cyclopsRight, new Vector2(-0.8f, 1.5f), Quaternion.identity);
        waveObjs[2] = Instantiate(cyclopsLeft, new Vector2(0.85f, 1.7f), Quaternion.identity);
        waveObjs[3] = Instantiate(cyclopsRight, new Vector2(-0.85f, 1.7f), Quaternion.identity);

        for(int i = 1; i < waveObjs.Length; i++) {
            waveObjs[i].GetComponent<Cyclops>().stopPoint -= 0.05f * i;
        }

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave3() {
        GameObject[] waveObjs = new GameObject[6];

        waveObjs[0] = Instantiate(cyclopsLeft, new Vector2(0.2f, 1.5f), Quaternion.identity);
        waveObjs[1] = Instantiate(cyclopsRight, new Vector2(-0.2f, 1.6f), Quaternion.identity);
        waveObjs[2] = Instantiate(cyclopsLeft, new Vector2(0.2f, 1.7f), Quaternion.identity);
        waveObjs[3] = Instantiate(cyclopsRight, new Vector2(-0.2f, 1.8f), Quaternion.identity);
        waveObjs[4] = Instantiate(cyclopsLeft, new Vector2(0.2f, 1.9f), Quaternion.identity);
        waveObjs[5] = Instantiate(cyclopsRight, new Vector2(-0.2f, 2f), Quaternion.identity);

        for (int i = 0; i < waveObjs.Length; i++) {
            waveObjs[i].GetComponent<Cyclops>().stopPoint = 0.5f - (0.08f * (float)i);
        }

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }
}
