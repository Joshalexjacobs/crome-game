using System.Collections;
using UnityEngine;

public class Planet : MonoBehaviour {

    public Sprite[] sprites;

    private SpriteRenderer sr;

	void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.position = new Vector2(Random.Range(-0.8f, 0.8f), Random.Range(0.8f, 0.95f));
    }

    public void StartMoveDown() {
        StartCoroutine("MoveDown");
    }

    IEnumerator MoveDown() {
        for (int i = 0; i < 40; i++) {
            transform.position = transform.position + new Vector3(0f, -0.01f, 0f);
            yield return new WaitForSeconds(0.1f);

            if (transform.position.y <= -0.73f) {
                sr.sprite = sprites[Random.Range(0, sprites.Length)];
                transform.position = new Vector2(Random.Range(-0.8f, 0.8f), Random.Range(0.8f, 0.95f));
            }
        }
    }
}
