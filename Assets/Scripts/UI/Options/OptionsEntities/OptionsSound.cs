using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSound : OptionsEntity {

    private OptionsSlider optionsSlider;

    public override void HandleEntity() {
        Debug.Log("HandleSoundEntity");
    }

    public override OptionsSlider GetSlider() {
        if (optionsSlider == null) {
            return GetComponentInChildren<OptionsSlider>();
        }

        return optionsSlider;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
