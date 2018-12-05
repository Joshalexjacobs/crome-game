using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {

    private SpriteRenderer sr;
    float maxHealth;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
    }
	
    public void StartFadeIn(float maxHealth) {
        this.maxHealth = maxHealth;
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn() {
        for (int i = 0; i < 5; i++) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.25f * i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void UpdateHealth(float health) {
        float y = health / maxHealth;
        sr.size = new Vector2(sr.size.x, y);
    }

    public void ResetHealthBar() {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        sr.size = new Vector2(sr.size.x, 1f);
    }
}
