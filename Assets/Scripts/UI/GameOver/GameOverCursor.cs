using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCursor : MonoBehaviour {

    public Vector2 retryPosition;
    public Vector2 exitPosition;

    private SpriteRenderer sr;
    private bool isActive = false;
    private bool movementReady = true;
    private string selection = "retry";

    private CromeController cromeController;

    // Use this for initialization
    void Start () {
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

    // Update is called once per frame
    void Update () {
        if (movementReady && isActive) {
            if (cromeController.CromeHorizontal() < 0f) { // left
                movementReady = false;
                StartCoroutine("ResetMovement");
                gameObject.transform.position = retryPosition;
                selection = "retry";
            } else if (cromeController.CromeHorizontal() > 0f) { // right
                movementReady = false;
                StartCoroutine("ResetMovement");
                gameObject.transform.position = exitPosition;
                selection = "exit";
            }

            if(cromeController.CromeIsFiring()) {
                HandleSelection();
            } else if(cromeController.CromeIsCanceling()) {
                SceneManager.LoadScene("title");
            }
        }
    }

    private void HandleSelection() {
        if(selection == "retry") {
            SceneManager.LoadScene("main");
        } else if(selection == "exit") {
            SceneManager.LoadScene("title");
        }
    }

    IEnumerator ResetMovement() {
        yield return new WaitForSeconds(0.05f);
        movementReady = true;
    }
}
