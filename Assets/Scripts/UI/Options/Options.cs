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
            entity.StartFadeIn();
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
