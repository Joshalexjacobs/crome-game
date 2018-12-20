using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public TutorialScreen startingTutorialScreen;

    private bool isReady = true;

    public void HandleTutorialFadeIn() {
        startingTutorialScreen.StartFadeIn();
    }

    public bool GetIsReady() {
        return isReady;
    }

    public void ResetTutorial() {
        isReady = false;
        StartCoroutine("ResetIsReady");
    }

    IEnumerator ResetIsReady() {
        yield return new WaitForSeconds(2f);
        isReady = true;
    }
}
