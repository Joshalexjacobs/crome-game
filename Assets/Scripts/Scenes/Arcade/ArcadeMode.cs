using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeMode : MonoBehaviour {

    void Start () {
        // changing the resolution...
        // Screen.SetResolution(480, 432, false); // set the resolution
        // set canvas scale factor to appropriate number (160 x 144 = 1, 320 x 288 = 2, 480 x 432 = 3 etc...)
        // make sure pixel perfect camera is set to 160 x 144 and upscale render texture is selected

        StartCoroutine("Arcade");
	}
	
    IEnumerator Arcade() {

        yield return new WaitForSeconds(0.8f);
    }

	void Update () {
        if (Input.GetKey("escape")) {
            SceneManager.LoadScene("title");
        }
    }

}