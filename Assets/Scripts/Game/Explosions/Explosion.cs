using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public Color[] colors;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();

        sr.color = colors[Random.Range(0, colors.Length)];

        StartCoroutine("Death");
	}
	
    IEnumerator Death() {
        yield return new WaitForSeconds(0.250f);
        Destroy(gameObject);
    }
}
