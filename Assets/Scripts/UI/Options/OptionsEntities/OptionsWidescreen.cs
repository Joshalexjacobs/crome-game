using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class OptionsWidescreen : OptionsEntity {

    private SpriteRenderer otherSr;
    private string widescreen = "Widescreen";
    private bool selectionReady = true;
    private PixelPerfectCamera ppCamera;

    public override void HandleEntity() {
        if (selectionReady) {
            ppCamera = GameObject.FindObjectOfType<PixelPerfectCamera>();

            ppCamera.cropFrameX = !ppCamera.cropFrameX;

            if (ppCamera.cropFrameX) {
                PlayerPrefs.SetInt(widescreen, 0);
                otherSr.color = new Color(otherSr.color.r, otherSr.color.g, otherSr.color.b, 0.4f);
            } else {
                PlayerPrefs.SetInt(widescreen, 1);
                otherSr.color = new Color(otherSr.color.r, otherSr.color.g, otherSr.color.b, 1f);
            }

            selectionReady = false;
            StartCoroutine("ResetSelection");
        }
    }

    void Update() {
        if (otherSr == null) {
            otherSr = GetComponent<SpriteRenderer>();
        }

        if (isActive) {
            ppCamera = GameObject.FindObjectOfType<PixelPerfectCamera>();

            if (ppCamera.cropFrameX) {
                otherSr.color = new Color(otherSr.color.r, otherSr.color.g, otherSr.color.b, 0.4f);
            } else {
                otherSr.color = new Color(otherSr.color.r, otherSr.color.g, otherSr.color.b, 1f);
            }
        }
    }

    IEnumerator ResetSelection() {
        yield return new WaitForSeconds(0.2f);
        selectionReady = true;
    }
}
