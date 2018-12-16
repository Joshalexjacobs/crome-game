using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class TitleScreen : MonoBehaviour {

    public TitleElement stumpheadGamesLogo;
    public TitleElement credits;
    public TitleElement stumpheadGamesLLC;
    public TitleElement cromeTitle;
    public TitleElement pressStart;
    public LeaderboardTicker leaderboardTicker;
    public TitleTextMesh currentTopThree;

    public bool steamBuild = false;

    private AudioSource[] audio;
    private bool hitStart = true;
    private InputDevice inputDevice;
    private CromeController cromeController;

    // Use this for initialization
    void Start () {
        audio = GetComponents<AudioSource>();
        cromeController = GetComponent<CromeController>();
        StartCoroutine("FadeInTitleScreen");
	}
	
    IEnumerator FadeInTitleScreen() {
        yield return new WaitForSeconds(1f);

        stumpheadGamesLogo.StartFadeIn();

        yield return new WaitForSeconds(1.5f);

        stumpheadGamesLogo.StartFadeOut();

        yield return new WaitForSeconds(1f);

        credits.StartFadeIn();

        yield return new WaitForSeconds(2f);

        credits.StartFadeOut();

        yield return new WaitForSeconds(1f);

        stumpheadGamesLLC.StartFadeIn();
        cromeTitle.StartFadeIn();
        pressStart.StartFadeIn();

        if(steamBuild) {
            leaderboardTicker.StartFadeIn();
            currentTopThree.StartFadeIn();
        }

        yield return new WaitForSeconds(1f);

        hitStart = false;
    }

    void Update() {
        inputDevice = InputManager.ActiveDevice;

        if (cromeController.CromeIsCanceling() && !hitStart) {
            Application.Quit();
        } else if (PressedStart() && !hitStart) {
            //StartCoroutine("PlayerHitStart");
            HandlePlayerStart();
        }
    }

    private bool PressedStart() {
        return inputDevice.GetControl(InputControlType.Options) || inputDevice.GetControl(InputControlType.Start) || inputDevice.Command.IsPressed
                          || cromeController.CromeIsFiring();
    }

    private void HandlePlayerStart() {
        TitleMenu titleMenu = GameObject.FindObjectOfType<TitleMenu>();
        titleMenu.FadeInTitleMenu();

        audio[1].Play();
        hitStart = true;
        pressStart.StartFadeOut(0.75f);
    }

    public void StartPlayerSelectedPlay() {
        StartCoroutine("PlayerSelectedPlay");
    }

    IEnumerator PlayerSelectedPlay() {
        StartCoroutine("FadeOutTitleTrack"); 
        audio[1].Play();
        hitStart = true;

        stumpheadGamesLLC.StartFadeOut(0.75f);
        cromeTitle.StartFadeOut(0.75f);
        pressStart.StartFadeOut(0.75f);

        if (steamBuild) {
            leaderboardTicker.StartFadeOut(0.75f);
            currentTopThree.StartFadeOut(0.75f);
        }

        Destroy(cromeController);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("main");
    }

    IEnumerator FadeOutTitleTrack() {
        while(audio[0].volume > 0) {
            audio[0].volume -= 0.1f;
            yield return new WaitForSeconds(0.5f);
        }

        audio[0].Stop();
    }
}
