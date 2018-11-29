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
    private string selection = "";

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
    }

    public void StartFadeIn() {
        isActive = true;
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn() {
        for (int i = 0; i < 10; i++) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void Update () {
        if (movementReady && isActive) {
            if (Input.GetAxis("Horizontal") < 0f) { // left
                movementReady = false;
                StartCoroutine("ResetMovement");
                gameObject.transform.position = retryPosition;
                selection = "retry";
            } else if (Input.GetAxis("Horizontal") > 0f) { // right
                movementReady = false;
                StartCoroutine("ResetMovement");
                gameObject.transform.position = exitPosition;
                selection = "exit";
            }

            if(Input.GetButton("Fire1")) {
                HandleSelection();
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
