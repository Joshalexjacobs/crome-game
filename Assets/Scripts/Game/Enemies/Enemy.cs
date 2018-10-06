using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 1f;
    public bool isDead = false;

    private SpriteRenderer sr;
    private BoxCollider2D box;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Damage(float damage) {
        health -= damage;

        if (health <= 0) {
            GetComponent<BoxCollider2D>().enabled = false;
            isDead = true;
            StartCoroutine("Death");
        } else {
            StartCoroutine("Flash");
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.SendMessage("Damage");
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

    public virtual IEnumerator Death() {
        ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
        explosionManager.AddExplosions(gameObject.transform.position);

        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(0.250f);

        Destroy(gameObject);
    }
}
