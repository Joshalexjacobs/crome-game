using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour {

    private Image image;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
    public void UpdateExperience(int experience) {
        image.fillAmount = 0.1f * (float)experience;
    }
}
