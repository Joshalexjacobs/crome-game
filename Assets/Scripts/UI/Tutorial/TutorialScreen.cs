using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour {

    public TutorialScreen nextScreen;

    private bool isReady = false;

    private SpriteRenderer sr;
    private CromeController cromeController;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
    }

    public void StartFadeIn() {
        StartCoroutine("FadeIn");
        cromeController = GameObject.FindObjectOfType<CromeController>();
    }

    IEnumerator FadeIn() {
        for (int i = 0; i <= 10; i++) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }

        isReady = true;
    }

    void Update() {
        if(isReady && cromeController != null) {
            if (cromeController.CromeIsFiring() || cromeController.CromeIsCanceling()) {
                StartFadeOut();
            }
        }
    }

    public void StartFadeOut() {
        isReady = false;
        StartCoroutine("FadeOut");
        //Destroy(cromeController);

        if(nextScreen != null) {
            nextScreen.StartFadeIn();
        } else {
            TitleMenuCursor titleMenuCursor = GameObject.FindObjectOfType<TitleMenuCursor>();
            titleMenuCursor.SetIsActive(true);

            Tutorial tutorial = GameObject.FindObjectOfType<Tutorial>();
            tutorial.ResetTutorial();
        }
    }

    IEnumerator FadeOut() {
        for (int i = 10; i >= 0; i--) {
            sr.color = new Color(1f, 1f, 1f, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }
}