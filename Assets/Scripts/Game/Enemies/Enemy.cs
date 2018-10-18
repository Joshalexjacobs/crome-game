using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int xp = 1;
    public int score = 10;
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

            if(score > 0) {
                ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
                scoreKeeper.AddToScore(score);
            }

            if(xp > 0) {
                Player player = FindObjectOfType<Player>();
                player.GainExperience(xp);
            }

            StartCoroutine("Death");
        } else {
            StartCoroutine("Flash");
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.SendMessage("Damage");
            StartCoroutine("HandlePlayerCollision");
        }
    }

    public virtual IEnumerator HandlePlayerCollision() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if(rb) {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            yield return new WaitForSeconds(0.2f);
            rb.isKinematic = false;
        }
    }

    public virtual IEnumerator Flash() {
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
