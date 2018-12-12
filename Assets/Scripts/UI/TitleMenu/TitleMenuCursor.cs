using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuCursor : MonoBehaviour {
    private static Vector3 MOVEMENT_DIFFERENCE = new Vector3(0f, 0.17f, 0f);
    private static Vector3 ORIGINAL_POSITION = new Vector3(-0.54f, 0.25f, 0f);


    private SpriteRenderer sr;
    private AudioSource audio;
    private bool isActive = false;
    private bool movementReady = true;
    private string selection = "";
    private int position = 1;

    private CromeController cromeController;

    // Use this for initialization
    void Start () {
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

    // Update is called once per frame
    void Update () {
        if (movementReady && isActive) {
            if (cromeController.CromeVertical() < 0f) { // down
                movementReady = false;
                StartCoroutine("ResetMovement");
                position++;

                if(position >= 5) {
                    position = 1;
                    gameObject.transform.position = ORIGINAL_POSITION;
                } else {
                    gameObject.transform.position -= MOVEMENT_DIFFERENCE;
                }

                audio.Play();
            } else if (cromeController.CromeVertical() > 0f) { // up
                movementReady = false;
                StartCoroutine("ResetMovement");
                position--;

                if (position <= 0) {
                    position = 4;
                    gameObject.transform.position = ORIGINAL_POSITION - MOVEMENT_DIFFERENCE * 3;
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

        switch(position) {
            case 1:
                isActive = false;
                Destroy(cromeController);
                GameObject.FindObjectOfType<TitleMenu>().FadeOutTitleMenu();
                TitleScreen titleScreen = GameObject.FindObjectOfType<TitleScreen>();
                titleScreen.StartPlayerSelectedPlay();
                break;
            case 2:
                Debug.Log("Leaderboard");
                break;
            case 3:
                Debug.Log("Options");
                break;
            case 4:
                Application.Quit();
                break;
        }
    }

    IEnumerator ResetMovement() {
        yield return new WaitForSeconds(0.2f);
        movementReady = true;
    }
}
