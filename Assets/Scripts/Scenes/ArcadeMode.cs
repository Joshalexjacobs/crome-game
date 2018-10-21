using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeMode : MonoBehaviour {

    public GameObject cyclopsFlyDownLeft;
    public GameObject cyclopsFlyDownRight;

    public GameObject cyclopsLeft;
    public GameObject cyclopsRight;

    public GameObject medusaLeft;
    public GameObject medusaRight;

    public GameObject shielder;

    public GameObject chopper;

    public GameObject fighter;
    public GameObject vulture;

    public GameObject mine;

    public GameObject antEater;

    // similar to supr crome, make a wave system but maybe categorize them based on enemy/enemy type
    // and then have a combo class that combines certain enemies

    void Start () {
        // changing the resolution...
        // Screen.SetResolution(480, 432, false); // set the resolution
        // set canvas scale factor to appropriate number (160 x 144 = 1, 320 x 288 = 2, 480 x 432 = 3 etc...)
        // make sure pixel perfect camera is set to 160 x 144 and upscale render texture is selected

        StartCoroutine("TestWave");
	}
	
	void Update () {
        if (Input.GetKey("escape")) {
            SceneManager.LoadScene("title");
        }
    }

    IEnumerator TestWave() {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(cyclopsFlyDownLeft, new Vector2(0f + i * -0.1f, 0f), Quaternion.identity).GetComponentInChildren<Cyclops>().SendMessage("FlyDown");
            yield return new WaitForSeconds(0.25f);
        }

        for (int i = 0; i < 3; i++)
        {
            Cyclops cyclopsLeftObj = Instantiate(cyclopsLeft, new Vector2(0.91f, 1.65f + i * 0.2f), Quaternion.identity).GetComponent<Cyclops>();
            Cyclops cyclopsRightObj = Instantiate(cyclopsRight, new Vector2(-0.91f, 1.65f + i * 0.2f), Quaternion.identity).GetComponent<Cyclops>();

            cyclopsLeftObj.stopPoint = 0.5f - i * 0.1f;
            cyclopsRightObj.stopPoint = 0.5f - i * 0.1f;

            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i < 3; i++)
        {
            Instantiate(cyclopsFlyDownRight, new Vector2(0f + i * 0.1f, 0f), Quaternion.identity).GetComponentInChildren<Cyclops>().SendMessage("FlyDown");
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(10f);

        Instantiate(shielder, new Vector2(0.6f, 0.3f), Quaternion.identity);
        Instantiate(shielder, new Vector2(-0.6f, 0.3f), Quaternion.identity);

        yield return new WaitForSeconds(7f);

        for (int i = 0; i < 3; i++) {
            Cyclops cyclopsLeftObj = Instantiate(cyclopsLeft, new Vector2(0.91f, 1.65f + i * 0.2f), Quaternion.identity).GetComponent<Cyclops>();
            Cyclops cyclopsRightObj = Instantiate(cyclopsRight, new Vector2(-0.91f, 1.65f + i * 0.2f), Quaternion.identity).GetComponent<Cyclops>();

            cyclopsLeftObj.stopPoint = 0.35f - i * 0.15f;
            cyclopsRightObj.stopPoint = 0.35f - i * 0.15f;

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(4f);

        Instantiate(medusaLeft, new Vector2(-1f, 0.15f), Quaternion.identity);
        Instantiate(medusaRight, new Vector2(1f, 0.15f), Quaternion.identity);

        yield return new WaitForSeconds(6f);

        Instantiate(chopper, new Vector2(0.5f, 1f), Quaternion.identity);

        yield return new WaitForSeconds(1f);

        Instantiate(chopper, new Vector2(-0.5f, 1f), Quaternion.identity);

        yield return new WaitForSeconds(15f);

        Instantiate(fighter, new Vector2(-0.7f, 1f), Quaternion.identity);
        Instantiate(fighter, new Vector2(0.7f, 1f), Quaternion.identity);

        yield return new WaitForSeconds(4f);

        Instantiate(vulture, new Vector2(0f, 1f), Quaternion.identity);

        yield return new WaitForSeconds(15f);

        SpawnMines(1);

        yield return new WaitForSeconds(3f);

        SpawnMines(2);

        yield return new WaitForSeconds(3f);

        SpawnMines(3);

        yield return new WaitForSeconds(3f);

        SpawnMines(4);

        yield return new WaitForSeconds(3f);

        Instantiate(antEater, new Vector2(Random.Range(-0.7f, 0.7f), Random.Range(1.0f, 1.6f)), Quaternion.identity);

        yield return new WaitForSeconds(2f);

        Instantiate(antEater, new Vector2(Random.Range(-0.7f, 0.7f), Random.Range(1.0f, 1.6f)), Quaternion.identity);
        Instantiate(antEater, new Vector2(Random.Range(-0.7f, 0.7f), Random.Range(1.0f, 1.6f)), Quaternion.identity);

        //StartCoroutine("TestWave");
    }

    private void SpawnMines(int x) {
        for (int i = 0; i < x; i++) {
            Instantiate(mine, new Vector2(Random.Range(-0.7f, 0.7f), Random.Range(1.0f, 1.6f)), Quaternion.identity);
        }
    }
}