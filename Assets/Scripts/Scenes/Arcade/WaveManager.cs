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

    public GameObject cyclopsFlyDownLeft;
    public GameObject cyclopsFlyDownRight;
    public GameObject cyclopsLeft;
    public GameObject cyclopsRight;

    public GameObject fighter;
    public GameObject medusaLeft;
    public GameObject medusaRight;
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

        float diff = Random.Range(0.04f, 0.09f);
        float baseStop = Random.Range(0.3f, 0.6f);

        for (int i = 0; i < waveObjs.Length; i++) {
            waveObjs[i].GetComponent<Cyclops>().stopPoint = baseStop - (diff * (float)i);
        }

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave4() {
        GameObject[] waveObjs = new GameObject[3];

        waveObjs[0] = Instantiate(cyclopsLeft, new Vector2(0.2f, 1.5f), Quaternion.identity);
        waveObjs[1] = Instantiate(cyclopsRight, new Vector2(-0.2f, 1.6f), Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        waveObjs[2] = Instantiate(antEater, new Vector2(0f, 1.6f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave5() {
        GameObject cyclopsToSpawn = cyclopsLeft;
        float direction = 1;

        if (Random.Range(0, 2) == 0) {
            cyclopsToSpawn = cyclopsRight;
            direction = -1;
        }

        GameObject[] waveObjs = new GameObject[4];

        waveObjs[0] = Instantiate(cyclopsToSpawn, new Vector2(direction * 0.8f, 1.5f), Quaternion.identity);
        waveObjs[1] = Instantiate(cyclopsToSpawn, new Vector2(direction * 0.9f, 1.6f), Quaternion.identity);
        waveObjs[2] = Instantiate(cyclopsToSpawn, new Vector2(direction * 1f, 1.7f), Quaternion.identity);

        for (int i = 1; i < 3; i++) {
            waveObjs[i].GetComponent<Cyclops>().stopPoint -= 0.05f * i;
        }

        yield return new WaitForSeconds(1.5f);

        waveObjs[3] = Instantiate(antEater, new Vector2(Random.Range(-0.6f, 0.6f), 1.6f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave6() {
        GameObject[] waveObjs = new GameObject[2];

        waveObjs[0] = Instantiate(chopper, new Vector2(-0.5f, 0.85f), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        waveObjs[1] = Instantiate(chopper, new Vector2(0.5f, 0.85f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave7() {
        GameObject[] waveObjs = new GameObject[3];

        waveObjs[0] = Instantiate(chopper, new Vector2(0f, 0.85f), Quaternion.identity);
        waveObjs[1] = Instantiate(antEater, new Vector2(-0.4f, 1.6f), Quaternion.identity);
        waveObjs[2] = Instantiate(antEater, new Vector2(0.4f, 1.6f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave8() {
        GameObject[] waveObjs = new GameObject[4];

        waveObjs[0] = Instantiate(chopper, new Vector2(-0.5f, 0.85f), Quaternion.identity);
        waveObjs[1] = Instantiate(chopper, new Vector2(0.5f, 0.85f), Quaternion.identity);

        yield return new WaitForSeconds(1f);

        waveObjs[2] = Instantiate(cyclopsLeft, new Vector2(0.2f, 1.5f), Quaternion.identity);
        waveObjs[3] = Instantiate(cyclopsRight, new Vector2(-0.2f, 1.6f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave9() {
        for(int j = 0; j < 3; j++) {
            for (int i = 0; i < 3; i++) {
                Instantiate(cyclopsFlyDownLeft, new Vector2(0f, 0f), Quaternion.identity).GetComponentInChildren<Cyclops>().FlyDown();
                yield return new WaitForSeconds(0.15f);
            }

            for (int i = 0; i < 3; i++) {
                Instantiate(cyclopsFlyDownRight, new Vector2(0f, 0f), Quaternion.identity).GetComponentInChildren<Cyclops>().FlyDown();
                yield return new WaitForSeconds(0.15f);
            }

            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(3f);

        this.HandleEndOfWave();
    }

    IEnumerator Wave10() {
        GameObject[] waveObjs = new GameObject[3];

        float xRange = Random.Range(0.2f, 0.7f);

        waveObjs[0] = Instantiate(fighter, new Vector2(0f, 1f), Quaternion.identity);
        waveObjs[1] = Instantiate(fighter, new Vector2(xRange, 2f), Quaternion.identity);
        waveObjs[2] = Instantiate(fighter, new Vector2(-xRange, 2f), Quaternion.identity);

        yield return new WaitForSeconds(5f);

        this.HandleEndOfWave();
    }

    IEnumerator Wave11() {
        GameObject[] waveObjs = new GameObject[5];

        float xRange = Random.Range(0.5f, 0.7f);

        waveObjs[0] = Instantiate(fighter, new Vector2(0f, 1f), Quaternion.identity);

        waveObjs[1] = Instantiate(fighter, new Vector2(0.3f, 2f), Quaternion.identity);
        waveObjs[2] = Instantiate(fighter, new Vector2(-0.3f, 2f), Quaternion.identity);

        waveObjs[3] = Instantiate(fighter, new Vector2(xRange, 3f), Quaternion.identity);
        waveObjs[4] = Instantiate(fighter, new Vector2(-xRange, 3f), Quaternion.identity);

        yield return new WaitForSeconds(5f);

        this.HandleEndOfWave();
    }

    IEnumerator Wave12() {
        GameObject[] waveObjs = new GameObject[2];

        Instantiate(fighter, new Vector2(0.3f, 2f), Quaternion.identity);
        Instantiate(fighter, new Vector2(-0.3f, 2f), Quaternion.identity);

        yield return new WaitForSeconds(1.2f);

        waveObjs[0] = Instantiate(antEater, new Vector2(-0.5f, 1.6f), Quaternion.identity);
        waveObjs[1] = Instantiate(antEater, new Vector2(0.5f, 1.6f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave13() {
        GameObject[] waveObjs = new GameObject[3];

        Instantiate(fighter, new Vector2(0.3f, 2f), Quaternion.identity);
        Instantiate(fighter, new Vector2(-0.3f, 2f), Quaternion.identity);

        yield return new WaitForSeconds(1.2f);

        waveObjs[0] = Instantiate(antEater, new Vector2(-0.5f, 1.6f), Quaternion.identity);
        waveObjs[1] = Instantiate(antEater, new Vector2(0.5f, 1.6f), Quaternion.identity);

        yield return new WaitForSeconds(0.6f);

        waveObjs[2] = Instantiate(antEater, new Vector2(0f, 1.6f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave14() {
        GameObject[] waveObjs = new GameObject[3];

        waveObjs[0] = Instantiate(chopper, new Vector2(0f, 0.85f), Quaternion.identity);
        waveObjs[1] = Instantiate(fighter, new Vector2(0.2f, 1.2f), Quaternion.identity);
        waveObjs[2] = Instantiate(fighter, new Vector2(-0.2f, 1.2f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave15() {
        GameObject[] waveObjs = new GameObject[5];

        waveObjs[0] = Instantiate(chopper, new Vector2(0f, 0.85f), Quaternion.identity);

        yield return new WaitForSeconds(0.6f);

        waveObjs[1] = Instantiate(chopper, new Vector2(0.5f, 0.85f), Quaternion.identity);
        waveObjs[2] = Instantiate(chopper, new Vector2(-0.5f, 0.85f), Quaternion.identity);

        yield return new WaitForSeconds(1f);

        waveObjs[3] = Instantiate(fighter, new Vector2(0.3f, 1.2f), Quaternion.identity);
        waveObjs[4] = Instantiate(fighter, new Vector2(-0.3f, 1.2f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave16() {
        GameObject[] waveObjs = new GameObject[6];

        Instantiate(fighter, new Vector2(Random.Range(0.2f, 0.7f), 1.2f), Quaternion.identity);
        Instantiate(fighter, new Vector2(Random.Range(-0.2f, -0.7f), 1.2f), Quaternion.identity);

        yield return new WaitForSeconds(2f);

        waveObjs[0] = Instantiate(cyclopsLeft, new Vector2(0.1f, 1.5f), Quaternion.identity);
        waveObjs[1] = Instantiate(cyclopsRight, new Vector2(-0.1f, 1.6f), Quaternion.identity);
        waveObjs[2] = Instantiate(cyclopsLeft, new Vector2(0.1f, 1.7f), Quaternion.identity);
        waveObjs[3] = Instantiate(cyclopsRight, new Vector2(-0.1f, 1.8f), Quaternion.identity);
        waveObjs[4] = Instantiate(cyclopsLeft, new Vector2(0.1f, 1.9f), Quaternion.identity);
        waveObjs[5] = Instantiate(cyclopsRight, new Vector2(-0.1f, 2f), Quaternion.identity);

        float diff = Random.Range(0.07f, 0.09f);
        float baseStop = Random.Range(0.3f, 0.45f);

        for (int i = 0; i < waveObjs.Length; i++) {
            waveObjs[i].GetComponent<Cyclops>().stopPoint = baseStop - (diff * (float)i);
        }

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave17() {
        GameObject[] waveObjs = new GameObject[2];

        waveObjs[0] = Instantiate(medusaLeft, new Vector2(-1f, Random.Range(-0.15f, 0.4f)), Quaternion.identity);
        waveObjs[1] = Instantiate(medusaRight, new Vector2(1f, Random.Range(-0.15f, 0.4f)), Quaternion.identity);

        for (int i = 0; i < waveObjs.Length; i++) {
            float stopPoint = Random.Range(0.2f, 0.65f);
            Medusa medusa = waveObjs[i].GetComponent<Medusa>();

            if(medusa.onRight) {
                medusa.stopPoint = stopPoint;
            } else {
                medusa.stopPoint = -stopPoint;
            }
        }

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave18() {
        GameObject medusaToSpawn = medusaLeft;
        float side = -1f;
        int numberOfMedusas = Random.Range(2, 6);

        if (Random.Range(0, 2) == 0) {
            medusaToSpawn = medusaRight;
            side = 1f;
        }

        GameObject[] waveObjs = new GameObject[numberOfMedusas];

        float yBase = Random.Range(0.35f, 0.4f);

        for(int i = 0; i < numberOfMedusas; i++) {
            float stopPoint = Random.Range(0.2f, 0.65f);
            waveObjs[i] = Instantiate(medusaToSpawn, new Vector2(side * 1f, yBase - (0.15f * (float)i)), Quaternion.identity);

            Medusa medusa = waveObjs[i].GetComponent<Medusa>();

            if (medusa.onRight) {
                medusa.stopPoint = stopPoint;
            } else {
                medusa.stopPoint = -stopPoint;
            }
        }

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
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
}
