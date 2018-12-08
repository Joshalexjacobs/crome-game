using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public Color[] colors;

    private SpriteRenderer sr;
    private GameObject camera;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        sr.color = colors[Random.Range(0, colors.Length)];
        camera.SendMessage ("BeginShortShake", 0.0045f);

        StartCoroutine("Death");
	}
	
    IEnumerator Death() {
        yield return new WaitForSeconds(0.250f);
        Destroy(gameObject);
    }
}
