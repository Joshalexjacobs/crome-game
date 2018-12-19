using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsFullscreen : OptionsEntity {

    private SpriteRenderer otherSr;

    private bool selectionReady = true;

    public override void HandleEntity() {
        if(selectionReady) {
            Screen.fullScreen = !Screen.fullScreen;

            if (!Screen.fullScreen) {
                otherSr.color = new Color(otherSr.color.r, otherSr.color.g, otherSr.color.b, 0.4f);
            } else {
                otherSr.color = new Color(otherSr.color.r, otherSr.color.g, otherSr.color.b, 1f);
            }

            selectionReady = false;
            StartCoroutine("ResetSelection");
        }
    }

    // Update is called once per frame
    void Update () {
        if(otherSr == null) {
            otherSr = GetComponent<SpriteRenderer>();
        }

        if(isActive) {
            if (!Screen.fullScreen) {
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
