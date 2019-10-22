using System.Collections;
using UnityEngine;

public class GenericParticle : MonoBehaviour {

    public Color[] colors;

    public float timeBeforeFade = 1f;
    public bool fadeOut = true;

    public bool fall = false;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();

        if(colors.Length > 0) {
            int newColor = Random.Range(0, colors.Length);
            sr.color = colors[newColor];
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }

        if(fadeOut) {
            StartCoroutine("FadeOut");
        }
	}
	
    IEnumerator FadeOut() {
        yield return new WaitForSeconds(timeBeforeFade);

        for (int i = 10; i >= 0; i--) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i * 0.1f);

            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }

    private void Update() {
        if(fall) {

        }
    }


}
