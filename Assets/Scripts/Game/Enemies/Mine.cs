using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Enemy {

    public float speed = 0.1f;

    public Mine() {
        health = 3;
        isDead = false;
    }

    private Rigidbody2D rb;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();

        rb.AddForce(Vector2.down * speed);

        StartCoroutine("Beep");
	}

    private void Update() {
        if(transform.position.y < -0.85f) {
            Destroy(gameObject);
        }
    }

    IEnumerator Beep() {
        while(!isDead) {
            yield return new WaitForSeconds(0.6f);
            audio.Play();
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.SendMessage("Damage");
            StartCoroutine("Death");
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
    }

    public override IEnumerator Death() {
        ExplosionManager explosionManager = GameObject.FindWithTag("ExplosionManager").GetComponent<ExplosionManager>();
        explosionManager.AddExplosions(gameObject.transform.position, 4, true, -0.07f, 0.07f);

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().enabled = false;

        Destroy(gameObject);
    }
}
