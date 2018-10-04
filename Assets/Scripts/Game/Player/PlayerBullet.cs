using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float speed = 1f;
    public float deathTime = 1.5f;

    private BoxCollider2D box;
    private Rigidbody2D rb;
    private Animator animator;
    private float damage = 1f;

	// Use this for initialization
	void Start () {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine("PlayerBulletDeath");
    }

    public void Init(float dmg) {
        if(rb == null) {
            rb = GetComponent<Rigidbody2D>();
        }

        rb.AddForce(new Vector2(0.0f, speed));
        this.damage = dmg;
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator PlayerBulletDeath() {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Enemy") {
            collision.gameObject.SendMessage("Damage", damage);
            rb.isKinematic = true;
            box.enabled = false;
            animator.SetBool("isDead", true);
            rb.velocity = new Vector2(0f, 0f);
            StartCoroutine("Death");
        }
    }

    IEnumerator Death() {
        yield return new WaitForSeconds(0.167f);
        Destroy(gameObject);
    }
}
