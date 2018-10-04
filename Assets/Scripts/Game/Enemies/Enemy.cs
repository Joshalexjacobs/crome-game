using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 1f;

    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Damage(float damage) {
        health -= damage;

        if (health <= 0) {
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine("Death");
        } else {
            StartCoroutine("Flash");
        }
    }

    IEnumerator Flash() {
        sr = GetComponent<SpriteRenderer>();

        for (int i = 0; i < 2; i++) {
            sr.material.SetFloat("_FlashAmount", 1);
            yield return new WaitForSeconds(0.035f);
            sr.material.SetFloat("_FlashAmount", 0);
            yield return new WaitForSeconds(0.035f);
        }
    }

    IEnumerator Death() {
        ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
        explosionManager.AddExplosions(gameObject.transform.position);

        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(0.250f);

        Destroy(gameObject);
    }
}
