using System.Collections;
using UnityEngine;

public class BackgroundStar : MonoBehaviour {

    public Color[] colors;

    public bool isTwinkling = false;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();

        sr.color = colors[Random.Range(0, colors.Length)];

        if(isTwinkling) {
            StartCoroutine("Twinkle");
        }
	}

    IEnumerator Twinkle() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(0.1f, 6.0f));

            for (int i = 10; i >= 0; i--) {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i * 0.1f);
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(1f);

            for (int i = 0; i <= 10; i++) {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i * 0.1f);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    public void StartMoveDown() {
        StartCoroutine("MoveDown");
    }

    IEnumerator MoveDown() {
        for (int i = 0; i < 40; i++) {
            transform.position = transform.position + new Vector3(0f, -0.01f, 0f);
            yield return new WaitForSeconds(0.1f);

            if(transform.position.y <= -0.73f) {
                sr.color = colors[Random.Range(0, colors.Length)];
                transform.position = new Vector2(Random.Range(-0.8f, 0.8f), Random.Range(0.73f, 0.83f));
            }
        }
    }
}
