using System.Collections;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float speed = 1f;
    public float deathTime = 1.5f;
    public bool sideBullet = false;
    public float sideFloat = 0.3f;

    private BoxCollider2D box;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource[] audio;
    private float damage = 1f;

	// Use this for initialization
	void Start () {
        box = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audio = GetComponents<AudioSource>();
        StartCoroutine("PlayerBulletDeath");
    }

    public void Init(float dmg) {
        if(rb == null) {
            rb = GetComponent<Rigidbody2D>();
        }

        if(sideBullet) {
            rb.AddForce(new Vector2(sideFloat, speed));
        } else {
            rb.AddForce(new Vector2(0.0f, speed));
        }
        
        this.damage = dmg;
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
        } else if (collision.tag == "Shield") {
            rb.isKinematic = true;
            box.enabled = false;
            animator.SetBool("isDead", true);
            rb.velocity = new Vector2(0f, 0f);
            StartCoroutine("Death");
        }
    }

    IEnumerator Death() {
        audio[0].pitch = Random.Range(0.65f, 1.00f);
        audio[0].Play();
        yield return new WaitForSeconds(0.167f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
