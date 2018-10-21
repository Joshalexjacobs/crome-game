using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

    public TitleElement stumpheadGamesLogo;
    public TitleElement stumpheadGamesLLC;
    public TitleElement cromeTitle;

	// Use this for initialization
	void Start () {
        StartCoroutine("FadeInTitleScreen");
	}
	
    IEnumerator FadeInTitleScreen() {
        yield return new WaitForSeconds(1f);

        stumpheadGamesLogo.StartFadeIn();

        yield return new WaitForSeconds(2f);

        stumpheadGamesLogo.StartFadeOut();

        yield return new WaitForSeconds(1f);

        stumpheadGamesLLC.StartFadeIn();
        cromeTitle.StartFadeIn();
    }

    void Update() {
        if (Input.GetKey("escape")) {
            Application.Quit();
        } else if (Input.GetButton("Fire1")) {
            SceneManager.LoadScene("main");
        }
    }
}
