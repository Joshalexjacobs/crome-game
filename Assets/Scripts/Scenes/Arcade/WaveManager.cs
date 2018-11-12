using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    // public vars

    public bool isTesting = false;
    public int testWave;
    public int testWave2;

    public int layersCompleted = 0;
    public bool deathCometDead = false;
    public int maxLevels;

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

    private int currentLevel = 1;
    private Stack<int> levelsStack;

    // Use this for initialization
    void Start () {
		if(isTesting) {
            StartCoroutine("TestWave");
        }

        levelsStack = new Stack<int>();
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

    public void StartNewWave(int phase) {
        StartCoroutine("NewWave", phase);
    }

    private string GetNextLevel() {
        string level = "";

        currentLevel++;

        if (levelsStack.Count == 0) {
            ResetLevelsStack();
        }

        level = levelsStack.Pop().ToString();

        return level;
    }

    private void ResetLevelsStack() {
        Debug.Log("Resetting Stack...");
        levelsStack.Clear();

        List<int> myValues = new List<int>();

        for (int i = 1; i <= maxLevels; i++) {
            myValues.Add(i);
        }

        IEnumerable<int> randomValues = myValues.Shuffle().Take(maxLevels);

        foreach (int item in randomValues) {
            levelsStack.Push(item);
        }
    }

    IEnumerator NewWave(int phase) {
        for(int i = 0; i < phase; i++) {
            //int nextWave = Random.Range(1, maxLevels);
            string nextWave = GetNextLevel();
            Debug.Log("Starting... " + nextWave);
            StartCoroutine("Wave" + nextWave.ToString());
            yield return new WaitForSeconds(GetPhaseWaitTime(phase));
        }
    }
    
    private float GetPhaseWaitTime(int phase) {
        float waitTime = 1f;

        switch(phase) {
            case 1:
                waitTime = 5f;
                break;
            case 2:
                waitTime = 4.5f;
                break;
            case 3:
                waitTime = 3f;
                break;
            case 4:
                waitTime = 2f;
                break;
            case 5:
                waitTime = 2.5f;
                break;
            case 6:
                waitTime = 2f;
                break;
            case 7:
                waitTime = 1.5f;
                break;
            default:
                waitTime = 1f;
                break;
        }

        return waitTime;
    }

    private void HandleEndOfWave() {
        layersCompleted++;
    }

    IEnumerator TestWave() {
        yield return new WaitForSeconds(3f);
        StartCoroutine("Wave" + testWave.ToString());

        if(testWave2 > 0) {
            yield return new WaitForSeconds(3f);
            StartCoroutine("Wave" + testWave2.ToString());
        }
    }

    public void StartDeathCometWave(int phase) {
        StartCoroutine("DeathComet", phase);
    }

    IEnumerator DeathComet(int phase) {
        GameObject[] waveObjs = new GameObject[1];

        waveObjs[0] = Instantiate(deathCometSpawn, new Vector2(0f, 0.3f), Quaternion.identity);
        waveObjs[0].GetComponent<DeathComet>().phase = phase;

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        yield return new WaitForSeconds(0.1f);

        waveObjs[0] = GameObject.FindObjectOfType<DeathComet>().gameObject;

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        deathCometDead = true;
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
        waveObjs[1] = Instantiate(antEater, new Vector2(-0.3f, 1.6f), Quaternion.identity);
        waveObjs[2] = Instantiate(antEater, new Vector2(0.3f, 1.6f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave8() {
        GameObject[] waveObjs = new GameObject[4];

        waveObjs[0] = Instantiate(chopper, new Vector2(-0.3f, 0.85f), Quaternion.identity);
        waveObjs[1] = Instantiate(chopper, new Vector2(0.3f, 0.85f), Quaternion.identity);

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

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

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

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave12() {
        GameObject[] waveObjs = new GameObject[4];

        waveObjs[0] = Instantiate(fighter, new Vector2(0.3f, 2f), Quaternion.identity);
        waveObjs[1] = Instantiate(fighter, new Vector2(-0.3f, 2f), Quaternion.identity);

        yield return new WaitForSeconds(1.2f);

        waveObjs[2] = Instantiate(antEater, new Vector2(-0.5f, 1.6f), Quaternion.identity);
        waveObjs[3] = Instantiate(antEater, new Vector2(0.5f, 1.6f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave13() {
        GameObject[] waveObjs = new GameObject[4];

        waveObjs[0] = Instantiate(fighter, new Vector2(0.3f, 2f), Quaternion.identity);
        waveObjs[1] = Instantiate(fighter, new Vector2(-0.3f, 2f), Quaternion.identity);

        yield return new WaitForSeconds(1.2f);

        waveObjs[2] = Instantiate(antEater, new Vector2(-0.5f, 1.6f), Quaternion.identity);
        waveObjs[3] = Instantiate(antEater, new Vector2(0.5f, 1.6f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave14() {
        GameObject[] waveObjs = new GameObject[2];

        waveObjs[0] = Instantiate(fighter, new Vector2(0.2f, 1.2f), Quaternion.identity);
        waveObjs[1] = Instantiate(fighter, new Vector2(-0.2f, 1.2f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave15() {
        GameObject[] waveObjs = new GameObject[2];

        waveObjs[0] = Instantiate(fighter, new Vector2(0.3f, 1.2f), Quaternion.identity);
        waveObjs[1] = Instantiate(fighter, new Vector2(-0.3f, 1.2f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave16() {
        GameObject[] waveObjs = new GameObject[8];

        waveObjs[0] = Instantiate(fighter, new Vector2(Random.Range(0.2f, 0.7f), 1.2f), Quaternion.identity);
        waveObjs[1] = Instantiate(fighter, new Vector2(Random.Range(-0.2f, -0.7f), 1.2f), Quaternion.identity);

        yield return new WaitForSeconds(2f);

        waveObjs[2] = Instantiate(cyclopsLeft, new Vector2(0.1f, 1.5f), Quaternion.identity);
        waveObjs[3] = Instantiate(cyclopsRight, new Vector2(-0.1f, 1.6f), Quaternion.identity);
        waveObjs[4] = Instantiate(cyclopsLeft, new Vector2(0.1f, 1.7f), Quaternion.identity);
        waveObjs[5] = Instantiate(cyclopsRight, new Vector2(-0.1f, 1.8f), Quaternion.identity);
        waveObjs[6] = Instantiate(cyclopsLeft, new Vector2(0.1f, 1.9f), Quaternion.identity);
        waveObjs[7] = Instantiate(cyclopsRight, new Vector2(-0.1f, 2f), Quaternion.identity);

        float diff = Random.Range(0.07f, 0.09f);
        float baseStop = Random.Range(0.3f, 0.45f);

        for (int i = 2; i < waveObjs.Length; i++) {
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

    IEnumerator Wave19() {
        int numberOfMines = Random.Range(7, 28);

        for(int i = 0; i < numberOfMines; i++) {
            Instantiate(mine, new Vector2(Random.Range(-0.7f, 0.7f), Random.Range(1.0f, 1.6f)), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0.15f, 0.8f));
        }

        yield return new WaitForSeconds(5f);

        this.HandleEndOfWave();
    }

    IEnumerator Wave20() {
        int numberOfMines = Random.Range(7, 28);

        for (int i = 0; i < 12; i++) {
            Instantiate(mine, new Vector2(-0.7f + (0.125f * (float)i), Random.Range(1.0f, 1.3f)), Quaternion.identity);
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(5f);

        this.HandleEndOfWave();
    }

    IEnumerator Wave21() {
        int numberOfMines = Random.Range(7, 28);

        for(int j = 0; j < 3; j++) {
            for (int i = 0; i < 10; i++) {
                Instantiate(mine, new Vector2(-0.7f + (0.15f * (float)i), Random.Range(1.0f, 1.4f)), Quaternion.identity);
                yield return new WaitForSeconds(0.025f);
            }

            yield return new WaitForSeconds(Random.Range(1f, 2.5f));
        }

        yield return new WaitForSeconds(5f);

        this.HandleEndOfWave();
    }

    IEnumerator Wave22() {
        GameObject[] waveObjs = new GameObject[3];

        waveObjs[0] = Instantiate(shielder, new Vector2(0f, 0.25f), Quaternion.identity);
        yield return new WaitForSeconds(0.75f);
        waveObjs[1] = Instantiate(shielder, new Vector2(-0.5f, 0.15f), Quaternion.identity);
        waveObjs[2] = Instantiate(shielder, new Vector2(0.5f, 0.15f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave23() {
        GameObject[] waveObjs = new GameObject[2];

        waveObjs[0] = Instantiate(shielder, new Vector2(-0.3f, 0.15f), Quaternion.identity);
        waveObjs[1] = Instantiate(shielder, new Vector2(0.3f, 0.15f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave24() {
        GameObject[] waveObjs = new GameObject[3];

        waveObjs[0] = Instantiate(shielder, new Vector2(0f, 0.25f), Quaternion.identity);
        waveObjs[1] = Instantiate(shielder, new Vector2(-0.25f, 0.2f), Quaternion.identity);
        waveObjs[2] = Instantiate(shielder, new Vector2(0.25f, 0.2f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave25() {
        GameObject[] waveObjs = new GameObject[3];

        waveObjs[0] = Instantiate(shielder, new Vector2(0f, 0.1f), Quaternion.identity);
        waveObjs[1] = Instantiate(shielder, new Vector2(-0.45f, 0.1f), Quaternion.identity);
        waveObjs[2] = Instantiate(shielder, new Vector2(0.45f, 0.1f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave26() {
        GameObject[] waveObjs = new GameObject[3];

        waveObjs[0] = Instantiate(shielder, new Vector2(0f, 0f), Quaternion.identity);
        waveObjs[1] = Instantiate(shielder, new Vector2(-0.6f, 0f), Quaternion.identity);
        waveObjs[2] = Instantiate(shielder, new Vector2(0.6f, 0f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave27() {
        GameObject[] waveObjs = new GameObject[2];

        waveObjs[0] = Instantiate(shielder, new Vector2(-0.6f, 0.25f), Quaternion.identity);
        waveObjs[1] = Instantiate(shielder, new Vector2(0.6f, 0.25f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    //vultures: 
    // 1st: 1 vulture random
    // 2nd: 1 vulture mid
    // 3rd: 2 vutlures random (staggered)
    // 4th: 2 vultures same time, seperate sides
    // 5th: 3 vutulures, 2 sides, 1 mid (staggered)

    IEnumerator Wave28() {
        GameObject[] waveObjs = new GameObject[1];

        waveObjs[0] = Instantiate(vulture, new Vector2(Random.Range(-0.65f, 0.65f), 1f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave29() {
        GameObject[] waveObjs = new GameObject[1];

        waveObjs[0] = Instantiate(vulture, new Vector2(0f, 1f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave30() {
        GameObject[] waveObjs = new GameObject[2];

        waveObjs[0] = Instantiate(vulture, new Vector2(Random.Range(-0.65f, -0.15f), 1f), Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        waveObjs[1] = Instantiate(vulture, new Vector2(Random.Range(0.15f, 0.65f), 1f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave31() {
        GameObject[] waveObjs = new GameObject[2];

        waveObjs[0] = Instantiate(vulture, new Vector2(-0.6f, 1f), Quaternion.identity);
        waveObjs[1] = Instantiate(vulture, new Vector2(0.6f, 1f), Quaternion.identity);

        while (LoopWhileAliveArray(waveObjs)) {
            yield return new WaitForSeconds(waveLoopTime);
        }

        this.HandleEndOfWave();
    }

    IEnumerator Wave32() {
        GameObject[] waveObjs = new GameObject[3];

        waveObjs[0] = Instantiate(vulture, new Vector2(-0.6f, 1f), Quaternion.identity);

        yield return new WaitForSeconds(2.5f);

        waveObjs[1] = Instantiate(vulture, new Vector2(0.6f, 1f), Quaternion.identity);

        yield return new WaitForSeconds(2.5f);

        waveObjs[2] = Instantiate(vulture, new Vector2(0f, 1f), Quaternion.identity);

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
