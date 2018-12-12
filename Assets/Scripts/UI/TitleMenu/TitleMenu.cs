using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour {

    public TitleMenuBG titleMenuBG;
    public TitleMenuCursor titleMenuCursor;

	// Use this for initialization
	void Start () {
		
	}
	
    public void FadeInTitleMenu() {
        titleMenuBG.StartFadeIn();
        titleMenuCursor.StartFadeIn();
    }

    public void FadeOutTitleMenu() {
        titleMenuBG.StartFadeOut();
        titleMenuCursor.StartFadeOut();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
