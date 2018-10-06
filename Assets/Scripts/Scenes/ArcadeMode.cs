using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeMode : MonoBehaviour {

    public GameObject cyclopsSpawnIn;
    public GameObject cyclopsSpawnInMirrored;

    public GameObject cyclopsLeft;
    public GameObject cyclopsRight;

    public GameObject medusaLeft;
    public GameObject medusaRight;

    public GameObject shielder;

    public GameObject chopper;

    void Start () {
        StartCoroutine("TestWave");
	}
	
	void Update () {
		
	}

    IEnumerator TestWave() {
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(cyclopsSpawnIn, new Vector2(1.002f, 1.482f), Quaternion.identity).SendMessage("FlyDown");
            yield return new WaitForSeconds(0.25f);
        }

        for (int i = 0; i < 3; i++)
        {
            Instantiate(cyclopsSpawnInMirrored, new Vector2(1.002f, 1.482f), Quaternion.identity).SendMessage("FlyDown");
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 3; i++) {
            Cyclops cyclopsLeftObj = Instantiate(cyclopsLeft, new Vector2(0.91f, 1.65f + i * 0.2f), Quaternion.identity).GetComponent<Cyclops>();
            Cyclops cyclopsRightObj = Instantiate(cyclopsRight, new Vector2(-0.91f, 1.65f + i * 0.2f), Quaternion.identity).GetComponent<Cyclops>();

            cyclopsLeftObj.stopPoint = 0.5f - i * 0.1f;
            cyclopsRightObj.stopPoint = 0.5f - i * 0.1f;

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(10f);

        Instantiate(shielder, new Vector2(0.6f, 0.3f), Quaternion.identity);
        Instantiate(shielder, new Vector2(-0.6f, 0.3f), Quaternion.identity);

        yield return new WaitForSeconds(15f);

        for (int i = 0; i < 3; i++) {
            Cyclops cyclopsLeftObj = Instantiate(cyclopsLeft, new Vector2(0.91f, 1.65f + i * 0.2f), Quaternion.identity).GetComponent<Cyclops>();
            Cyclops cyclopsRightObj = Instantiate(cyclopsRight, new Vector2(-0.91f, 1.65f + i * 0.2f), Quaternion.identity).GetComponent<Cyclops>();

            cyclopsLeftObj.stopPoint = 0.35f - i * 0.15f;
            cyclopsRightObj.stopPoint = 0.35f - i * 0.15f;

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(3f);

        Instantiate(medusaLeft, new Vector2(-1f, 0.15f), Quaternion.identity);
        Instantiate(medusaRight, new Vector2(1f, 0.15f), Quaternion.identity);

        yield return new WaitForSeconds(2f);

        Instantiate(chopper, new Vector2(0f, 1f), Quaternion.identity);

        yield return new WaitForSeconds(2f);

        Instantiate(chopper, new Vector2(0.5f, 1f), Quaternion.identity);

        yield return new WaitForSeconds(2f);

        Instantiate(chopper, new Vector2(-0.6f, 1f), Quaternion.identity);
    }

}