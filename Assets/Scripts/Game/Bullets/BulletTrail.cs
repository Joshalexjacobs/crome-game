using System.Collections;
using UnityEngine;

public class BulletTrail : MonoBehaviour {

    public bool isShrinking = false;
    public float shrinkTime = 0.25f;

    private SpriteRenderer sr;

    void Start() {
        if (isShrinking) {
            StartCoroutine("Shrink");
        }
    }

    public void Init(Color color) {
        sr = GetComponent<SpriteRenderer>();
        sr.color = color;
    }

    IEnumerator Shrink() {
        for (int i = 0; i < 5; i++) {
            transform.localScale = new Vector3(transform.localScale.x - 0.125f, transform.localScale.y - 0.125f, transform.localScale.z);
            yield return new WaitForSeconds(shrinkTime);
        }

        Destroy(gameObject);
    }
}