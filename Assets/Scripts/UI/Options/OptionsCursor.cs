using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsCursor : MonoBehaviour {
    private static Vector3 MOVEMENT_DIFFERENCE = new Vector3(0f, 0.15f, 0f);
    private static Vector3 ORIGINAL_POSITION = new Vector3(-0.125f, 0.5f, 0f);

    public OptionsEntity [] optionsEntities;

    private SpriteRenderer sr;
    private AudioSource audio;
    private bool isActive = false;
    private bool movementReady = true;
    private int position = 0;
    private int positionMax = 3;

    private CromeController cromeController;

    // Use this for initialization
    void Start () {
        positionMax = optionsEntities.Length;
        audio = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
    }

    public void StartFadeIn() {
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn() {
        for (int i = 0; i < 10; i++) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }

        cromeController = GameObject.FindObjectOfType<CromeController>();
        isActive = true;
    }

    public void StartFadeOut() {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut() {
        for (int i = 4; i >= 0; i--) {
            sr.color = new Color(1f, 1f, 1f, 0.25f * i);
            yield return new WaitForSeconds(0.75f);
        }
    }

    void Update() {
        if (movementReady && isActive) {
            if (cromeController.CromeVertical() < 0f) { // down
                movementReady = false;
                StartCoroutine("ResetMovement");
                position++;

                if (position >= positionMax) {
                    position = 0;
                    gameObject.transform.position = ORIGINAL_POSITION;
                } else {
                    gameObject.transform.position -= MOVEMENT_DIFFERENCE;
                }

                audio.Play();
            } else if (cromeController.CromeVertical() > 0f) { // up
                movementReady = false;
                StartCoroutine("ResetMovement");
                position--;

                if (position < 0) {
                    position = positionMax - 1;
                    gameObject.transform.position = ORIGINAL_POSITION - MOVEMENT_DIFFERENCE * (positionMax - 1);
                } else {
                    gameObject.transform.position += MOVEMENT_DIFFERENCE;
                }

                audio.Play();
            }

            if (cromeController.CromeIsFiring()) {
                HandleSelection();
            }
            //else if (cromeController.CromeIsCanceling()) {
            //    SceneManager.LoadScene("title");
            //}
        }
    }

    private void HandleSelection() {
        //isActive = false;

        optionsEntities[position].HandleEntity();

        //switch (position) {
        //    case 1:
        //        optionsEntities[position].HandleEntity();
        //        break;
        //}
    }

    IEnumerator ResetMovement() {
        yield return new WaitForSeconds(0.2f);
        movementReady = true;
    }

    public void DestroyCromeController() {
        if(cromeController != null) {
            Destroy(cromeController);
        }
    }
}
