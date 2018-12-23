using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsSlider : MonoBehaviour {

    public string playerPrefsField = "";
    public AudioMixer audioMixer;

    private TextMesh textMesh;
    private MeshRenderer mr;

    private int volume = 100;

	// Use this for initialization
	void Start () {
        mr = GetComponent<MeshRenderer>();
        textMesh = GetComponent<TextMesh>();
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0f);

        mr.sortingLayerName = "Menu";
        mr.sortingOrder = 15;

        volume = PlayerPrefs.GetInt(playerPrefsField);

        if(volume == 0) {
            volume = 100;
        } else if (volume == -1) {
            volume = 0;
        }

        textMesh.text = volume.ToString();
        audioMixer.SetFloat(playerPrefsField, volume - 100);
    }

    public void StartFadeIn() {
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn() {
        for (int i = 0; i <= 10; i++) {
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void StartFadeOut() {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut() {
        for (int i = 10; i >= 0; i--) {
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0.1f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void IncreaseVolume(int amount) {
        if(amount + volume >= 0 && amount + volume <= 100) {
            volume += amount;
            textMesh.text = volume.ToString();

            if(volume == 0) {
                PlayerPrefs.SetInt(playerPrefsField, -1);
            } else {
                PlayerPrefs.SetInt(playerPrefsField, volume);
            }

            audioMixer.SetFloat(playerPrefsField, (((float)volume * 0.01f) * 60f) - 60f);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}