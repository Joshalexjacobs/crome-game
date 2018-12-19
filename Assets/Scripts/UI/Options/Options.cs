using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour {

	public bool optionsActive = false;
    public OptionsBG optionsBG;
    public OptionsEntity[] optionsEntities;
    public OptionsCursor optionsCursor;

    // Use this for initialization
    void Start () {
		
	}

    public void SetOptionsActive(bool optionsActive) {
        this.optionsActive = optionsActive;

        if(optionsActive) {
            optionsBG.StartFadeIn();
            optionsCursor.StartFadeIn();
            FadeInEntities();
        }
    }
	
    private void FadeInEntities() {
        foreach(OptionsEntity entity in optionsEntities) {
            Debug.Log(entity);
            entity.StartFadeIn();
        }
    }

    public void SetOptionsInactive() {
        this.optionsActive = false;
        optionsBG.StartFadeOut();
        optionsCursor.StartFadeOut();
        FadeOutEntities();

        TitleMenuCursor titleMenuCursor = GameObject.FindObjectOfType<TitleMenuCursor>();
        titleMenuCursor.SetIsActive(true);
    }

    private void FadeOutEntities() {
        foreach (OptionsEntity entity in optionsEntities) {
            entity.StartFadeOut();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
