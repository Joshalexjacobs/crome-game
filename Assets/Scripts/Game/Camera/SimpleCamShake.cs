using System.Collections;
using UnityEngine;

public class SimpleCamShake : MonoBehaviour {

    public Camera mainCamera;
    public Vector3 originalCameraPosition = new Vector3(0f, 0f, -10f);

    float shakeAmt = 0f;
    bool verticalOnly = false;
    bool horizontalOnly = false;

    public void BeginShortestShake(float shakeAmount) {
        shakeAmt = shakeAmount;
        verticalOnly = false;
        horizontalOnly = false;

        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.1f);
    }

    public void BeginShortShake(float shakeAmount) {
        shakeAmt = shakeAmount;
        verticalOnly = false;
        horizontalOnly = false;

        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.2f);
    }

    public void BeginShortVerticalShake(float shakeAmount) {
        shakeAmt = shakeAmount;
        verticalOnly = true;
        horizontalOnly = false;

        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.1f);
    }

    public void BeginMedVerticalShake(float shakeAmount) {
        shakeAmt = shakeAmount;
        verticalOnly = true;
        horizontalOnly = false;

        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);
    }

    public void BeginMedShake(float shakeAmount) {
        shakeAmt = shakeAmount;
        verticalOnly = false;
        horizontalOnly = false;

        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.6f);
    }

    public void BeginBigShake(float shakeAmount) {
        shakeAmt = shakeAmount;
        verticalOnly = false;
        horizontalOnly = false;

        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.9f);
    }

    IEnumerator CameraShake(float times) {
        for (float i = 0.0f; i < times; i += 0.01f) {
            if (shakeAmt > 0) {
                float quakeAmt = Random.Range(-0.50f, 0.50f) * shakeAmt * 2;
                Vector3 pp = mainCamera.transform.position;

                if (verticalOnly)
                    pp.y += quakeAmt;
                else if (horizontalOnly)
                    pp.x += quakeAmt;
                else {
                    pp.x += quakeAmt;
                    pp.y += quakeAmt;
                }

                mainCamera.transform.position = pp;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    void CameraShake() {
        if (shakeAmt > 0) {
            float quakeAmt = Random.Range(-0.50f, 0.50f) * shakeAmt * 2;
            Vector3 pp = mainCamera.transform.position;

            if (verticalOnly)
                pp.y += quakeAmt;
            else if (horizontalOnly)
                pp.x += quakeAmt;
            else {
                pp.x += quakeAmt;
                pp.y += quakeAmt;
            }

            mainCamera.transform.position = pp;
        }
    }

    void StopShaking() {
        CancelInvoke("CameraShake");

        verticalOnly = false;
        horizontalOnly = false;

        mainCamera.transform.position = originalCameraPosition;
    }
}
